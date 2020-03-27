using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Menus : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        private MenuType index
        {
            get
            {
                return (MenuType)menuName.SelectedIndex;
            }
            set
            {
                menuName.SelectedIndex = (int)value;
            }
        }
        private PaletteSet bgPaletteSet
        {
            get
            {
                if (index == MenuType.GameSelect)
                    return Model.GameSelectBGPalette;
                else if (index < MenuType.Shop)
                    return Model.MenuBGPalette;
                else
                    return Model.ShopBGPalette;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    Model.GameSelectBGPalette = value;
                else if (index < MenuType.Shop)
                    Model.MenuBGPalette = value;
                else
                    Model.ShopBGPalette = value;
            }
        }
        private PaletteSet fgPaletteSet
        {
            get
            {
                if (index == MenuType.GameSelect)
                    return Model.GameSelectPaletteSet;
                else
                    return Model.FontPaletteMenu;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    Model.GameSelectPaletteSet = value;
                else
                    Model.FontPaletteMenu = value;
            }
        }
        private byte[] fgGraphics
        {
            get
            {
                if (index == MenuType.GameSelect)
                    return Model.GameSelectGraphics;
                else
                    return Model.MenuFrameGraphics;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    Model.GameSelectGraphics = value;
                else
                    Model.MenuFrameGraphics = value;
            }
        }
        private PaletteSet cursorPaletteSet { get { return Model.CursorPaletteSet; } set { Model.CursorPaletteSet = value; } }
        private Bitmap bgImage;
        private Bitmap fgImage;
        private Bitmap stereoImage;
        private Bitmap monoImage;
        private Bitmap cursorImage;
        private Bitmap previewImage;
        private bool editTileset;
        private Bitmap[] allyImages;
        private Bitmap[] cursorImages;
        public CursorSprite[] CursorSprites;
        private CursorSprite cursorSprite
        {
            get
            { return CursorSprites[cursorName.SelectedIndex]; }
            set
            { CursorSprites[cursorName.SelectedIndex] = value; }
        }
        private Settings settings = Settings.Default;
        public PictureBox Picture { get { return pictureBoxPreview; } set { pictureBoxPreview = value; } }
        //
        private List<TextObject> textObjects;
        private int mouseOverTextObject = -1;
        //
                // editors
        private MenusEditor menusEditor;
        private PaletteEditor bgPaletteEditor;
        private GraphicEditor bgGraphicEditor;
        private PaletteEditor fgPaletteEditor;
        private GraphicEditor fgGraphicEditor;
        private PaletteEditor cursorsPaletteEditor;
        private GraphicEditor cursorsGraphicEditor;
        private PaletteEditor speakersPaletteEditor;
        private GraphicEditor speakersGraphicEditor;
        #endregion
        public Menus(MenusEditor menusEditor)
        {
            this.menusEditor = menusEditor;
            InitializeComponent();
            this.music.Items.AddRange(Lists.Numerize(Lists.MusicNames));
            this.music.SelectedIndex = Model.ROM[0x03462D];
            //
            cursorSpriteNum.Items.AddRange(Lists.Resize(Lists.Numerize(3, Lists.SpriteNames), 256));
            CursorSprites = new CursorSprite[5];
            for (int i = 0; i < CursorSprites.Length; i++)
                CursorSprites[i] = new CursorSprite(i);
            cursorName.SelectedIndex = 0;
            //
            this.Updating = true;
            index = MenuType.GameSelect;
            this.Updating = false;
            //
            SetAllyImages();
            SetBackgroundImage();
            SetForegroundImage();
            SetSpeakersImages();
            SetCursorImages();
            SetPreviewImage();
            SetTextObjects();
            //
            this.History = new History(this, menuName, null);
        }
        public void Reload(MenusEditor menusEditor)
        {
            this.music.Items.AddRange(Lists.Numerize(Lists.MusicNames));
            this.music.SelectedIndex = Model.ROM[0x03462D];
            //
            cursorSpriteNum.Items.AddRange(Lists.Resize(Lists.Numerize(3, Lists.SpriteNames), 256));
            CursorSprites = new CursorSprite[5];
            for (int i = 0; i < CursorSprites.Length; i++)
                CursorSprites[i] = new CursorSprite(i);
            cursorName.SelectedIndex = 0;
            //
            this.Updating = true;
            index = MenuType.GameSelect;
            this.Updating = false;
            //
            SetAllyImages();
            SetBackgroundImage();
            SetForegroundImage();
            SetSpeakersImages();
            SetCursorImages();
            SetPreviewImage();
            SetTextObjects();
            //
            GC.Collect();
        }
        #region Functions
        private void RefreshMenu()
        {
            toolStrip2.Visible = index == MenuType.GameSelect;
            music.Visible = index == MenuType.GameSelect;
            labelMusic.Visible = index == MenuType.GameSelect;
            toolStripSeparator2.Visible = index == MenuType.GameSelect;
            importFGToolStripMenuItem.Text = index == MenuType.GameSelect ? "Import Foreground" : "Import Frame";
            openPalettesFG.Text = index == MenuType.GameSelect ? "Foreground Palettes" : "Frame Palette";
            openGraphicsFG.Text = index == MenuType.GameSelect ? "Foreground Graphics" : "Frame Graphics";
            if (bgGraphicEditor != null)
                LoadBGGraphicEditor();
            if (fgGraphicEditor != null)
                LoadFGGraphicEditor();
            if (bgPaletteEditor != null)
                LoadBGPaletteEditor();
            if (fgPaletteEditor != null)
                LoadFGPaletteEditor();
            SetBackgroundImage();
            SetForegroundImage();
            SetPreviewImage();
            SetTextObjects();
        }
        private void SetCursorImage()
        {
            Size size = new Size(0, 0);
            Sprite sprite = Model.Sprites[cursorSprite.Sprite];
            Animation animation = Model.Animations[sprite.AnimationPacket];
            Sequence sequence = null;
            if (cursorSprite.Sequence < animation.Sequences.Count)
                sequence = animation.Sequences[cursorSprite.Sequence];
            int moldIndex = -1;
            if (sequence != null && sequence.Frames.Count > 0)
                moldIndex = sequence.Frames[0].Mold;
            if (moldIndex != -1)
            {
                int[] pixels = sprite.GetPixels(true, false, moldIndex, 0, null, false, true, ref size);
                cursorImage = Do.PixelsToImage(pixels, size.Width, size.Height);
            }
            else
                cursorImage = new Bitmap(1, 1);
        }
        private void SetCursorImages()
        {
            cursorImages = new Bitmap[5];
            int[] cursor = Do.GetPixelRegion(Model.MenuCursorGraphics, 0x20, cursorPaletteSet.Palette, 16, 0, 0, 3, 2, 0);
            int[] pageRight = Do.GetPixelRegion(Model.MenuCursorGraphics, 0x20, cursorPaletteSet.Palette, 16, 3, 0, 2, 2, 0);
            int[] upArrow = Do.GetPixelRegion(Model.MenuCursorGraphics, 0x20, cursorPaletteSet.Palette, 16, 5, 0, 2, 2, 0);
            int[] downArrow = Do.GetPixelRegion(Model.MenuCursorGraphics, 0x20, cursorPaletteSet.Palette, 16, 7, 0, 2, 2, 0);
            int[] pageDown = Do.GetPixelRegion(Model.MenuCursorGraphics, 0x20, cursorPaletteSet.Palette, 16, 9, 0, 2, 2, 0);
            cursorImages[0] = Do.PixelsToImage(cursor, 24, 16);
            cursorImages[1] = Do.PixelsToImage(pageRight, 16, 16);
            cursorImages[2] = Do.PixelsToImage(upArrow, 16, 16);
            cursorImages[3] = Do.PixelsToImage(downArrow, 16, 16);
            cursorImages[4] = Do.PixelsToImage(pageDown, 16, 16);
            pictureBoxPreview.Invalidate();
        }
        private void SetAllyImages()
        {
            allyImages = new Bitmap[5];
            for (int i = 0; i < allyImages.Length; i++)
            {
                Size size = new Size(0, 0);
                int index = Model.ROM[0x0318A3 + i];
                Sprite sprite = Model.Sprites[index];
                Animation animation = Model.Animations[sprite.AnimationPacket];
                Sequence sequence = null;
                int sequenceIndex = Model.ROM[0x031881];
                if (sequenceIndex < animation.Sequences.Count)
                    sequence = animation.Sequences[sequenceIndex];
                int moldIndex = 0;
                if (sequence != null && sequence.Frames.Count >= 0)
                    moldIndex = sequence.Frames[0].Mold;
                int[] pixels = sprite.GetPixels(true, false, moldIndex, 0, false, false, ref size);
                allyImages[i] = Do.PixelsToImage(pixels, size.Width, size.Height);
            }
        }
        private void SetBackgroundImage()
        {
            if (index == MenuType.GameSelect)
                bgImage = Model.GameBG;
            else if (index < MenuType.Shop)
                bgImage = Model.MenuBG;
            else
                bgImage = Model.ShopBG;
            pictureBoxBG.Invalidate();
        }
        private void SetForegroundImage()
        {
            if (index == MenuType.GameSelect)
            {
                Tile[] tileset = new MenuTileset(fgPaletteSet, Model.GameSelectTileset, fgGraphics).Tileset;
                int[] pixels = Do.TilesetToPixels(tileset, 16, 16, 0, false);
                fgImage = Do.PixelsToImage(pixels, 256, 256);
                pictureBoxFG.Size = new Size(256, 256);
            }
            else
            {
                fgImage = Do.PixelsToImage(
                    Do.DrawMenuFrame(new Size(5, 6), fgGraphics, fgPaletteSet.Palette), 40, 48);
                pictureBoxFG.Size = new Size(40, 48);
            }
            pictureBoxFG.Invalidate();
            pictureBoxPreview.Invalidate();
        }
        private void SetSpeakersImages()
        {
            int[] pixels = Do.GetPixelRegion(Model.GameSelectSpeakers, 0x20, fgPaletteSet.Palettes[14], 16, 8, 0, 7, 3, 0);
            stereoImage = Do.PixelsToImage(pixels, 56, 24);
            pixels = Do.GetPixelRegion(Model.GameSelectSpeakers, 0x20, fgPaletteSet.Palettes[15], 16, 0, 0, 7, 3, 0);
            monoImage = Do.PixelsToImage(pixels, 56, 24);
            pictureBoxPreview.Invalidate();
        }
        private void SetPreviewImage()
        {
            if (index == MenuType.GameSelect)
            {
                pictureBoxPreview.Invalidate();
                return;
            }
            int[] bgPixels;
            if (index < MenuType.Shop)
                bgPixels = Do.ImageToPixels(Model.MenuBG);
            else
                bgPixels = Do.ImageToPixels(Model.ShopBG);
            Rectangle[] frames;
            switch (index)
            {
                default: frames = new Rectangle[] // Overworld
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(136, 7, 14, 15),
                        new Rectangle(144, 127, 12, 11)
                    };
                    break;
                case MenuType.OverworldItem: frames = new Rectangle[] // Overworld - Items
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(8, 151, 15, 8),
                        new Rectangle(8, 151, 15, 3),
                        new Rectangle(128, 7, 17, 25)
                    };
                    break;
                case MenuType.OverworldStatus: frames = new Rectangle[] // Overworld - status
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(128, 7, 15, 25)
                    };
                    break;
                case MenuType.OverworldSpecial: frames = new Rectangle[] // Overworld - special
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(128, 7, 15, 11),
                        new Rectangle(128, 103, 15, 3),
                        new Rectangle(128, 127, 15, 11)
                    };
                    break;
                case MenuType.OverworldEquip: frames = new Rectangle[] // Overworld - equip
                    {
                        new Rectangle(0, 7, 17, 6),
                        new Rectangle(0, 55, 17, 6),
                        new Rectangle(0, 103, 17, 6),
                        new Rectangle(0, 151, 17, 8),
                        new Rectangle(136, 7, 17, 25)
                    };
                    break;
                case MenuType.OverworldSpecialItem: frames = new Rectangle[] // Overworld - special item
                    {
                        new Rectangle(8, 39, 30, 14),
                        new Rectangle(64, 151, 16, 6)
                    };
                    break;
                case MenuType.OverworldSwitch: frames = new Rectangle[] // Overworld - switch
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(128, 55, 15, 6),
                        new Rectangle(128, 103, 15, 6),
                        new Rectangle(136, 159, 13, 6)
                    };
                    break;
                case MenuType.Shop: frames = new Rectangle[] // Shop
                    {
                        new Rectangle(8, 7, 15, 8),
                        new Rectangle(8, 79, 15, 3),
                        new Rectangle(128, 7, 15, 26)
                    };
                    break;
                case MenuType.ShopBuy: frames = new Rectangle[] // Shop - buy
                    {
                        new Rectangle(8, 7, 15, 15),
                        new Rectangle(8, 127, 15, 8),
                        new Rectangle(128, 7, 15, 25)
                    };
                    break;
                case MenuType.ShopSellItems: frames = new Rectangle[] // Shop - sell items
                    {
                        new Rectangle(8, 7, 15, 15),
                        new Rectangle(128, 7, 17, 25)
                    };
                    break;
                case MenuType.ShopSellWeapons: frames = new Rectangle[] // Shop - sell weapons
                    {
                        new Rectangle(8, 7, 15, 15),
                        new Rectangle(8, 127, 15, 8),
                        new Rectangle(128, 7, 17, 25)
                    };
                    break;
            }
            foreach (Rectangle frame in frames)
                Do.DrawMenuFrame(bgPixels, 256, frame, Model.MenuFrameGraphics, Model.FontPaletteMenu.Palette);
            previewImage = Do.PixelsToImage(bgPixels, 256, 224);
            pictureBoxPreview.Invalidate();
        }
        public void SetTextObjects()
        {
            textObjects = new List<TextObject>();
            switch (index)
            {
                case 0:
                    textObjects.Add(new TextObject(43, 95, 24));
                    textObjects.Add(new TextObject(109, 47, 56));
                    textObjects.Add(new TextObject(110, 47, 96));
                    textObjects.Add(new TextObject(111, 47, 136));
                    textObjects.Add(new TextObject(112, 47, 176));
                    break;
                case MenuType.Shop:
                    textObjects.Add(new TextObject(76, 23, 12));
                    textObjects.Add(new TextObject(77, 23, 24));
                    textObjects.Add(new TextObject(78, 23, 36));
                    textObjects.Add(new TextObject(0, 23, 48));
                    //
                    textObjects.Add(new TextObject(27, 23, 84));
                    textObjects.Add(new TextObject(32, 23, 192));
                    break;
                case MenuType.ShopBuy:
                    textObjects.Add(new TextObject(76, 15, 12));
                    textObjects.Add(new TextObject(80, 15, 72));
                    textObjects.Add(new TextObject(27, 15, 96));
                    textObjects.Add(new TextObject(79, 15, 108));
                    break;
                case MenuType.ShopSellItems:
                    textObjects.Add(new TextObject(77, 15, 12));
                    textObjects.Add(new TextObject(80, 15, 72));
                    textObjects.Add(new TextObject(27, 15, 96));
                    textObjects.Add(new TextObject(79, 15, 108));
                    break;
                case MenuType.ShopSellWeapons:
                    textObjects.Add(new TextObject(78, 15, 12));
                    textObjects.Add(new TextObject(80, 15, 72));
                    textObjects.Add(new TextObject(27, 15, 96));
                    textObjects.Add(new TextObject(79, 15, 108));
                    break;
                default:
                    textObjects.Add(new TextObject(31, 39, 24));
                    textObjects.Add(new TextObject(12, 39, 36));
                    textObjects.Add(new TextObject(31, 39, 72));
                    textObjects.Add(new TextObject(12, 39, 84));
                    textObjects.Add(new TextObject(31, 39, 120));
                    textObjects.Add(new TextObject(12, 39, 132));
                    if (index == MenuType.OverworldMain) // Overworld - main
                    {
                        textObjects.Add(new TextObject(1, 143, 12));
                        textObjects.Add(new TextObject(3, 143, 24));
                        textObjects.Add(new TextObject(2, 143, 36));
                        textObjects.Add(new TextObject(0, 143, 48));
                        textObjects.Add(new TextObject(4, 143, 60));
                        textObjects.Add(new TextObject(5, 143, 72));
                        textObjects.Add(new TextObject(6, 143, 84));
                        textObjects.Add(new TextObject(7, 143, 96));
                        textObjects.Add(new TextObject(8, 143, 108));
                        //
                        textObjects.Add(new TextObject(55, 151, 132));
                        textObjects.Add(new TextObject(27, 151, 156));
                        textObjects.Add(new TextObject(56, 151, 180));
                    }
                    else if (index == MenuType.OverworldItem) // Overworld - items
                    {
                        textObjects.Add(new TextObject(55, 15, 156));
                    }
                    else if (index == MenuType.OverworldStatus) // Overworld - status
                    {
                        textObjects.Add(new TextObject(19, Model.MenuTexts[19].X * 8 - 2, 12));
                        textObjects.Add(new TextObject(15, Model.MenuTexts[15].X * 8 - 2, 48));
                        textObjects.Add(new TextObject(16, Model.MenuTexts[16].X * 8 - 2, 72));
                        textObjects.Add(new TextObject(17, Model.MenuTexts[17].X * 8 - 2, 96));
                        textObjects.Add(new TextObject(18, Model.MenuTexts[18].X * 8 - 2, 120));
                        textObjects.Add(new TextObject(14, Model.MenuTexts[14].X * 8 - 2, 156));
                    }
                    else if (index == MenuType.OverworldSpecial) // Overworld - special
                    {
                        textObjects.Add(new TextObject(55, 135, 108));
                    }
                    else if (index == MenuType.OverworldSwitch) // Overworld - switch
                    {
                        textObjects.Add(new TextObject(31, 159, 72));
                        textObjects.Add(new TextObject(12, 159, 84));
                        //
                        textObjects.Add(new TextObject(31, 159, 120));
                        textObjects.Add(new TextObject(12, 159, 132));
                        //
                        textObjects.Add(new TextObject(14, 15, 156));
                        textObjects.Add(new TextObject(20, 143, 168));
                        textObjects.Add(new TextObject(21, 151, 180));
                    }
                    break;
            }
        }
        private void LoadBGPaletteEditor()
        {
            if (bgPaletteEditor == null)
            {
                bgPaletteEditor = new PaletteEditor(new Function(BGPaletteUpdate), bgPaletteSet, 1, 0, 1);
                bgPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                bgPaletteEditor.Reload(new Function(BGPaletteUpdate), bgPaletteSet, 1, 0, 1);
        }
        private void LoadBGGraphicEditor()
        {
            if (bgGraphicEditor == null)
            {
                bgGraphicEditor = new GraphicEditor(new Function(BGGraphicUpdate),
                    Model.MenuBGGraphics, Model.MenuBGGraphics.Length, 0, bgPaletteSet, 0, 0x20);
                bgGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                bgGraphicEditor.Reload(new Function(BGGraphicUpdate),
                    Model.MenuBGGraphics, Model.MenuBGGraphics.Length, 0, bgPaletteSet, 0, 0x20);
        }
        private void LoadFGPaletteEditor()
        {
            if (fgPaletteEditor == null)
            {
                if (index == MenuType.GameSelect)
                    fgPaletteEditor = new PaletteEditor(new Function(FGPaletteUpdate), fgPaletteSet, 16, 1, 5);
                else
                    fgPaletteEditor = new PaletteEditor(new Function(FGPaletteUpdate), fgPaletteSet, 1, 0, 1);
                fgPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
            {
                if (index == MenuType.GameSelect)
                    fgPaletteEditor.Reload(new Function(FGPaletteUpdate), fgPaletteSet, 16, 1, 5);
                else
                    fgPaletteEditor.Reload(new Function(FGPaletteUpdate), fgPaletteSet, 1, 0, 1);
            }
        }
        private void LoadFGGraphicEditor()
        {
            if (fgGraphicEditor == null)
            {
                fgGraphicEditor = new GraphicEditor(new Function(FGGraphicUpdate),
                    fgGraphics, fgGraphics.Length, 0, fgPaletteSet, 0, (byte)(index == MenuType.GameSelect ? 0x20 : 0x10));
                fgGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                fgGraphicEditor.Reload(new Function(FGGraphicUpdate),
                    fgGraphics, fgGraphics.Length, 0, fgPaletteSet, 0, (byte)(index == MenuType.GameSelect ? 0x20 : 0x10));
        }
        private void LoadCursorsPaletteEditor()
        {
            if (cursorsPaletteEditor == null)
            {
                cursorsPaletteEditor = new PaletteEditor(new Function(CursorsPaletteUpdate), cursorPaletteSet, 1, 0, 1);
                cursorsPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                cursorsPaletteEditor.Reload(new Function(CursorsPaletteUpdate), cursorPaletteSet, 1, 0, 1);
        }
        private void LoadCursorsGraphicEditor()
        {
            if (cursorsGraphicEditor == null)
            {
                cursorsGraphicEditor = new GraphicEditor(new Function(BGGraphicUpdate),
                    Model.MenuCursorGraphics, Model.MenuCursorGraphics.Length, 0, cursorPaletteSet, 0, 0x20);
                cursorsGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                cursorsGraphicEditor.Reload(new Function(CursorsGraphicUpdate),
                    Model.MenuCursorGraphics, Model.MenuCursorGraphics.Length, 0, cursorPaletteSet, 0, 0x20);
        }
        private void LoadSpeakersPaletteEditor()
        {
            if (speakersPaletteEditor == null)
            {
                speakersPaletteEditor = new PaletteEditor(new Function(SpeakersPaletteUpdate), fgPaletteSet, 16, 14, 2);
                speakersPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                speakersPaletteEditor.Reload(new Function(SpeakersPaletteUpdate), fgPaletteSet, 16, 14, 2);
        }
        private void LoadSpeakersGraphicEditor()
        {
            if (speakersGraphicEditor == null)
            {
                speakersGraphicEditor = new GraphicEditor(new Function(SpeakersGraphicUpdate),
                    Model.GameSelectSpeakers, Model.GameSelectSpeakers.Length, 0, fgPaletteSet, 14, 0x20);
                speakersGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                speakersGraphicEditor.Reload(new Function(SpeakersGraphicUpdate),
                    Model.GameSelectSpeakers, Model.GameSelectSpeakers.Length, 0, fgPaletteSet, 14, 0x20);
        }
        private void BGPaletteUpdate()
        {
            Model.MenuBG = null;
            Model.ShopBG = null;
            Model.GameBG = null;
            SetBackgroundImage();
            SetPreviewImage();
            LoadBGGraphicEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void BGGraphicUpdate()
        {
            Model.MenuBG = null;
            Model.ShopBG = null;
            Model.GameBG = null;
            SetBackgroundImage();
            SetPreviewImage();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void FGPaletteUpdate()
        {
            SetForegroundImage();
            SetPreviewImage();
            LoadFGGraphicEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void FGGraphicUpdate()
        {
            SetForegroundImage();
            SetPreviewImage();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void CursorsPaletteUpdate()
        {
            SetCursorImages();
            SetPreviewImage();
            LoadCursorsGraphicEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void CursorsGraphicUpdate()
        {
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void SpeakersPaletteUpdate()
        {
            SetSpeakersImages();
            LoadSpeakersGraphicEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void SpeakersGraphicUpdate()
        {
            SetSpeakersImages();
            this.Modified = true;   // b/c switching colors won't modify
        }
        //
        private void ImportBackground(Bitmap import)
        {
            if (import.Width > 256 || import.Height > 256)
            {
                MessageBox.Show(
                    "The dimensions of the imported image must be no larger than 256x256.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            byte[] graphics = new byte[0x8000];
            int[] pixels = Do.ImageToPixels(import, new Size(256, 256), new Rectangle(0, 0, 256, 256));
            int[] palette = Do.ReduceColorDepth(pixels, 16, 0);
            for (int i = 0; i < palette.Length; i++)
            {
                bgPaletteSet.Reds[i] = Color.FromArgb(palette[i]).R;
                bgPaletteSet.Greens[i] = Color.FromArgb(palette[i]).G;
                bgPaletteSet.Blues[i] = Color.FromArgb(palette[i]).B;
            }
            Do.PixelsToBPP(pixels, graphics, new Size(256 / 8, 256 / 8), palette, 0x20);
            //
            byte[] tileset = new byte[0x800];
            byte[] temp = new byte[graphics.Length]; graphics.CopyTo(temp, 0);
            int size = Do.CopyToTileset(graphics, tileset, palette, 5, true, false, 0x20, 2, new Size(256, 256), 0x100);
            //
            Buffer.BlockCopy(tileset, 0, Model.MenuBGTileset, 0, 0x800);
            Buffer.BlockCopy(graphics, 0, Model.MenuBGGraphics, 0, 0x2000);
            if (size > 8192)
                MessageBox.Show("Not enough space to store the necessary amount of SNES graphics data for the imported images. The total required space (" +
                    size + " bytes) for the new SNES graphics data exceeds 8192 bytes.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //
            Model.MenuBG = null;
            Model.ShopBG = null;
            Model.GameBG = null;
            SetBackgroundImage();
            SetPreviewImage();
            LoadBGGraphicEditor();
            LoadBGPaletteEditor();
            this.Modified = true;
        }
        private void ImportForeground(Bitmap import)
        {
            if (index == MenuType.GameSelect)
            {
                if (import.Width > 256 || import.Height > 256)
                {
                    MessageBox.Show(
                        "The dimensions of the imported image must be no larger than 256 x 256.",
                        "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //
                byte[] graphics = new byte[0x8000];
                int[] pixels = Do.ImageToPixels(import, new Size(256, 256), new Rectangle(0, 0, 256, 256));
                int[] palette = Do.ReduceColorDepth(pixels, 16, 0);
                for (int i = 0; i < palette.Length; i++)
                {
                    fgPaletteSet.Reds[16 * 4 + i] = Color.FromArgb(palette[i]).R;
                    fgPaletteSet.Greens[16 * 4 + i] = Color.FromArgb(palette[i]).G;
                    fgPaletteSet.Blues[16 * 4 + i] = Color.FromArgb(palette[i]).B;
                }
                Do.PixelsToBPP(pixels, graphics, new Size(256 / 8, 256 / 8), palette, 0x20);
                //
                byte[] tileset = new byte[0x800];
                byte[] temp = new byte[graphics.Length]; graphics.CopyTo(temp, 0);
                int size = Do.CopyToTileset(graphics, tileset, palette, 4, true, false, 0x20, 2, new Size(256, 256), 0);
                //
                Buffer.BlockCopy(tileset, 0, Model.GameSelectTileset, 0, 0x800);
                Buffer.BlockCopy(graphics, 0, Model.GameSelectGraphics, 0, 0x2000);
                if (size > 8192)
                    MessageBox.Show("Not enough space to store the necessary amount of SNES graphics data for the imported images. The total required space (" +
                        size + " bytes) for the new SNES graphics data exceeds 8192 bytes.",
                        "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                editTileset = true;
            }
            else
            {
                if (import.Width != 40 || import.Height != 48)
                {
                    MessageBox.Show(
                        "The dimensions of the imported image must be exactly 40 x 48.",
                        "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //
                byte[] graphics = new byte[0x1E0];
                int[] pixels = Do.ImageToPixels(import, new Size(40, 48), new Rectangle(0, 0, 40, 48));
                int[] palette = Do.ReduceColorDepth(pixels, 4, 0);
                Do.PixelsToBPP(pixels, graphics, new Size(5, 6), palette, 0x10);
                for (int i = 0; i < palette.Length; i++)
                {
                    Model.FontPaletteMenu.Reds[i] = Color.FromArgb(palette[i]).R;
                    Model.FontPaletteMenu.Greens[i] = Color.FromArgb(palette[i]).G;
                    Model.FontPaletteMenu.Blues[i] = Color.FromArgb(palette[i]).B;
                }
                // top
                for (int a = 0; a < 0x50; a++)
                    Model.MenuFrameGraphics[a] = graphics[a];
                for (int a = 0x100, b = 0x50; a < 0x150 && b < 0xA0; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                // bottom
                for (int a = 0x170, b = 0x190; a < 0x1C0 && b < 0x1E0; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                for (int a = 0x70, b = 0x140; a < 0xC0 && b < 0x190; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                // sides
                for (int a = 0x50, b = 0xA0; a < 0x60 && b < 0xB0; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                for (int a = 0x60, b = 0xE0; a < 0x70 && b < 0xF0; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                for (int a = 0x150, b = 0xF0; a < 0x160 && b < 0x100; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                for (int a = 0x160, b = 0x130; a < 0x160 && b < 0x140; a++, b++)
                    Model.MenuFrameGraphics[a] = graphics[b];
                //
            }
            SetForegroundImage();
            SetPreviewImage();
            LoadFGGraphicEditor();
            LoadFGPaletteEditor();
            this.Modified = true;
        }
        //
        public void Assemble()
        {
            Model.ROM[0x03462D] = (byte)this.music.SelectedIndex;
            Model.Compress(Model.GameSelectGraphics, 0x3E9A49, 0x2000, 0x3EB2CD - 0x3E9A49, "Game select graphics");
            if (editTileset)
                Model.Compress(Model.GameSelectTileset, 0x3EB2CD, 0x800, 0x3EB50F - 0x3EB2CD, "Game select tileset");
            Model.Compress(Model.GameSelectPalettes, 0x3EB50F, 0x200, 0x3EB624 - 0x3EB50F, "Game select palettes");
            Model.Compress(Model.GameSelectSpeakers, 0x3EB624, 0x600, 0x3EB94A - 0x3EB624, "Game select speakers");
            //
            for (int i = 0; i < CursorSprites.Length; i++)
                CursorSprites[i].Assemble();
            //
            Model.GameSelectPaletteSet.Assemble();
            Model.GameSelectBGPalette.Assemble();
            Model.FontPaletteMenu.Assemble(Model.MenuPalettes, 0);
            Model.MenuBGPalette.Assemble();
            Model.ShopBGPalette.Assemble();
            Model.CursorPaletteSet.Assemble(Model.MenuPalettes, 0x100);
            int offset = 0x4C;
            byte[] dst = new byte[0x2E81];
            // copy uncompressed world map logo graphics
            Bits.SetShort(dst, 0x00, 0x4C);
            Buffer.BlockCopy(Model.ROM, 0x3E004C, dst, 0x4C, 0xE1C);
            offset += 0xE1C;
            //
            Bits.SetShort(dst, 0x02, offset);
            if (!Model.Compress(Model.MenuBGGraphics, dst, ref offset, 0x2E81, "BG graphics")) return;
            Bits.SetShort(dst, 0x04, offset);
            if (!Model.Compress(Model.MenuFrameGraphics, dst, ref offset, 0x2E81, "Frame graphics")) return;
            Bits.SetShort(dst, 0x06, offset);
            if (!Model.Compress(Model.MenuCursorGraphics, dst, ref offset, 0x2E81, "Cursor graphics")) return;
            Bits.SetShort(dst, 0x08, offset);
            if (!Model.Compress(Model.MenuBGTileset, dst, ref offset, 0x2E81, "BG tileset")) return;
            Bits.SetShort(dst, 0x0A, offset);
            if (!Model.Compress(Model.UNKTileset3E2C80, dst, ref offset, 0x2E81, "UNK tileset")) return;
            Bits.SetShort(dst, 0x0C, offset);
            if (!Model.Compress(Model.MenuPalettes, dst, ref offset, 0x2E81, "Menu palettes")) return;
            // set pointers (just the first 7 for menu data)
            Buffer.BlockCopy(dst, 0, Model.ROM, 0x3E0000, 0x0E);
            // store compressed data (starting at start of data)
            Buffer.BlockCopy(dst, 0x4C, Model.ROM, 0x3E004C, dst.Length - 0x4C);
        }
        public new void Close()
        {
            if (bgPaletteEditor != null)
            {
                bgPaletteEditor.Close();
                bgPaletteEditor.Dispose();
            }
            if (bgGraphicEditor != null)
            {
                bgGraphicEditor.Close();
                bgGraphicEditor.Dispose();
            }
            if (fgPaletteEditor != null)
            {
                fgPaletteEditor.Close();
                fgPaletteEditor.Dispose();
            }
            if (fgGraphicEditor != null)
            {
                fgGraphicEditor.Close();
                fgGraphicEditor.Dispose();
            }
            if (cursorsGraphicEditor != null)
            {
                cursorsGraphicEditor.Close();
                cursorsGraphicEditor.Dispose();
            }
            if (cursorsPaletteEditor != null)
            {
                cursorsPaletteEditor.Close();
                cursorsPaletteEditor.Dispose();
            }
            if (speakersGraphicEditor != null)
            {
                speakersGraphicEditor.Close();
                speakersGraphicEditor.Dispose();
            }
            if (speakersPaletteEditor != null)
            {
                speakersPaletteEditor.Close();
                speakersPaletteEditor.Dispose();
            }
        }
        #endregion
        #region Event Handlers
        private void pictureBoxBG_Paint(object sender, PaintEventArgs e)
        {
            if (bgImage == null)
                return;
            e.Graphics.DrawImage(bgImage, 0, 0);
        }
        private void pictureBoxFG_Paint(object sender, PaintEventArgs e)
        {
            if (fgImage != null)
                e.Graphics.DrawImage(fgImage, 0, 0);
        }
        private void pictureBoxPreview_Paint(object sender, PaintEventArgs e)
        {
            if (index != MenuType.GameSelect && previewImage != null)
                e.Graphics.DrawImage(previewImage, 0, 0);
            int[] pal = Model.FontPaletteMenu.Palette;
            MenuTextPreview pre = new MenuTextPreview();
            //
            switch (index)
            {
                case 0: // game select
                    if (bgImage != null)
                        e.Graphics.DrawImage(bgImage, 0, 0);
                    if (fgImage != null)
                        e.Graphics.DrawImage(fgImage, 0, 0);
                    if (stereoImage != null)
                        e.Graphics.DrawImage(stereoImage, 8, 15);
                    if (monoImage != null)
                        e.Graphics.DrawImage(monoImage, 192, 17);
                    //
                    Do.DrawText(Model.MenuTexts[43].ToString(), 95, 24, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[109].ToString(), 47, 56, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[110].ToString(), 47, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[111].ToString(), 47, 136, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[112].ToString(), 47, 176, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    if (cursorImage == null)
                        SetCursorImage();
                    e.Graphics.DrawImage(cursorImage, 28, 55);
                    break;
                case MenuType.OverworldEquip: // Overworld - equip
                    if (allyImages != null)
                    {
                        e.Graphics.DrawImage(allyImages[0], 16 - 128, 12 - 96 - 1);
                        e.Graphics.DrawImage(allyImages[2], 16 - 128, 60 - 96 - 1);
                        e.Graphics.DrawImage(allyImages[4], 16 - 128, 108 - 96 - 1);
                    }
                    Do.DrawText(Model.Items[33].ToString(), 23, 12, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.Items[69].ToString(), 23, 24, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.Items[81].ToString(), 23, 36, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    Do.DrawText(Model.Items[30].ToString(), 23, 60, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.Items[67].ToString(), 23, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.Items[94].ToString(), 23, 84, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    Do.DrawText(Model.Items[32].ToString(), 23, 108, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.Items[65].ToString(), 23, 120, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.Items[90].ToString(), 23, 132, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    if (cursorImages != null)
                    {
                        e.Graphics.DrawImage(cursorImages[0], 4, 13);
                        e.Graphics.DrawImage(cursorImages[1], 232, 191);
                        e.Graphics.DrawImage(cursorImages[4], 60, 141);
                    }
                    break;
                case MenuType.OverworldSpecialItem: // Overworld - special item
                    if (cursorImages != null)
                        e.Graphics.DrawImage(cursorImages[0], 0, 49);
                    break;
                case MenuType.Shop: // Shop
                    Do.DrawText(Model.MenuTexts[76].ToString(), 23, 12, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[77].ToString(), 23, 24, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[78].ToString(), 23, 36, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[0].ToString(), 23, 48, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    Do.DrawText(Model.MenuTexts[27].ToString(), 23, 84, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[32].ToString(), 23, 192, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 87, 84, e.Graphics, pre, Model.FontMenu, pal);
                    e.Graphics.DrawImage(cursorImages[0], 4, 13);
                    break;
                case MenuType.ShopBuy: // Shop - Buy
                    Do.DrawText(Model.MenuTexts[76].ToString(), 15, 12, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[80].ToString(), 15, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 95, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[27].ToString(), 15, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 95, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[79].ToString(), 15, 108, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("99", 103, 108, e.Graphics, pre, Model.FontMenu, pal);
                    e.Graphics.DrawImage(cursorImages[0], 116, 13);
                    break;
                case MenuType.ShopSellItems: // Shop - Sell Items
                    Do.DrawText(Model.MenuTexts[77].ToString(), 15, 12, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[80].ToString(), 15, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 95, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[27].ToString(), 15, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 95, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[79].ToString(), 15, 108, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("99", 103, 108, e.Graphics, pre, Model.FontMenu, pal);
                    e.Graphics.DrawImage(cursorImages[0], 116, 13);
                    break;
                case MenuType.ShopSellWeapons: // Shop - Sell Weapons
                    Do.DrawText(Model.MenuTexts[78].ToString(), 15, 12, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[80].ToString(), 15, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 95, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[27].ToString(), 15, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("999", 95, 96, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[79].ToString(), 15, 108, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("99", 103, 108, e.Graphics, pre, Model.FontMenu, pal);
                    e.Graphics.DrawImage(cursorImages[0], 116, 13);
                    break;
                default:
                    if (allyImages != null)
                    {
                        e.Graphics.DrawImage(allyImages[0], 28 - 128, 12 - 96 - 1);
                        e.Graphics.DrawImage(allyImages[2], 28 - 128, 60 - 96 - 1);
                        e.Graphics.DrawImage(allyImages[4], 28 - 128, 108 - 96 - 1);
                    }
                    //
                    Do.DrawText(Model.Characters[0].ToString(), 39, 12, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[31].ToString(), 39, 24, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[12].ToString(), 39, 36, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("30          ", 71, 24, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("209/209     ", 63, 36, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    Do.DrawText(Model.Characters[2].ToString(), 39, 60, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[31].ToString(), 39, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[12].ToString(), 39, 84, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("30          ", 71, 72, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("240/240     ", 63, 84, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    Do.DrawText(Model.Characters[4].ToString(), 39, 108, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[31].ToString(), 39, 120, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText(Model.MenuTexts[12].ToString(), 39, 132, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("30          ", 71, 120, e.Graphics, pre, Model.FontMenu, pal);
                    Do.DrawText("195/195     ", 63, 132, e.Graphics, pre, Model.FontMenu, pal);
                    //
                    if (index == MenuType.OverworldMain) // Overworld - main
                    {
                        Do.DrawText(Model.MenuTexts[1].ToString(), 143, 12, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[3].ToString(), 143, 24, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[2].ToString(), 143, 36, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[0].ToString(), 143, 48, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[4].ToString(), 143, 60, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[5].ToString(), 143, 72, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[6].ToString(), 143, 84, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[7].ToString(), 143, 96, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[8].ToString(), 143, 108, e.Graphics, pre, Model.FontMenu, pal);
                        //
                        Do.DrawText(Model.MenuTexts[55].ToString(), 151, 132, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("99/99       ", 191, 145, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[27].ToString(), 151, 156, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("999         ", 207, 169, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[56].ToString(), 151, 180, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("999         ", 207, 193, e.Graphics, pre, Model.FontMenu, pal);
                        //
                        if (cursorImages != null)
                            e.Graphics.DrawImage(cursorImages[0], 124, 13);
                    }
                    else if (index == MenuType.OverworldItem) // Overworld - items
                    {
                        Do.DrawText(Model.MenuTexts[55].ToString(), 15, 156, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("99/99       ", 79, 156, e.Graphics, pre, Model.FontMenu, pal);
                        if (cursorImages != null)
                        {
                            e.Graphics.DrawImage(cursorImages[0], 116, 13);
                            e.Graphics.DrawImage(cursorImages[1], 232, 191);
                        }
                    }
                    else if (index == MenuType.OverworldStatus) // Overworld - status
                    {
                        Do.DrawText(Model.MenuTexts[19].ToString(), Model.MenuTexts[19].X * 8 - 2, 12, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[15].ToString(), Model.MenuTexts[15].X * 8 - 2, 48, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[16].ToString(), Model.MenuTexts[16].X * 8 - 2, 72, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[17].ToString(), Model.MenuTexts[17].X * 8 - 2, 96, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[18].ToString(), Model.MenuTexts[18].X * 8 - 2, 120, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[14].ToString(), Model.MenuTexts[14].X * 8 - 2, 156, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 12, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 24, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 48, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 60, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 72, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 84, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 96, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 108, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("255", 216, 120, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("9999", 208, 168, e.Graphics, pre, Model.FontMenu, pal);
                        if (cursorImages != null)
                            e.Graphics.DrawImage(cursorImages[0], 4, 23);
                    }
                    else if (index == MenuType.OverworldSpecial) // Overworld - special
                    {
                        Do.DrawText(Model.MenuTexts[55].ToString(), 135, 108, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("99/99       ", 200, 108, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.Spells[0].ToString(), 135, 12, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.Spells[1].ToString(), 135, 24, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.Spells[2].ToString(), 135, 36, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.Spells[3].ToString(), 135, 48, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.Spells[4].ToString(), 135, 60, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.Spells[5].ToString(), 135, 72, e.Graphics, pre, Model.FontMenu, pal);
                        if (cursorImages != null)
                            e.Graphics.DrawImage(cursorImages[0], 4, 23);
                    }
                    else if (index == MenuType.OverworldSwitch) // Overworld - switch
                    {
                        if (allyImages != null)
                        {
                            e.Graphics.DrawImage(allyImages[1], 148 - 128, 60 - 96 - 1);
                            e.Graphics.DrawImage(allyImages[3], 148 - 128, 108 - 96 - 1);
                        }
                        //
                        Do.DrawText(Model.Characters[1].ToString(), 159, 60, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[31].ToString(), 159, 72, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[12].ToString(), 159, 84, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("30          ", 191, 72, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("211/211     ", 183, 84, e.Graphics, pre, Model.FontMenu, pal);
                        //
                        Do.DrawText(Model.Characters[3].ToString(), 159, 108, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[31].ToString(), 159, 120, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[12].ToString(), 159, 132, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("30          ", 191, 120, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("199/199     ", 183, 132, e.Graphics, pre, Model.FontMenu, pal);
                        //
                        Do.DrawText(Model.MenuTexts[14].ToString(), 15, 156, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText("9999", 87, 168, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[20].ToString(), 143, 168, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(Model.MenuTexts[21].ToString(), 151, 180, e.Graphics, pre, Model.FontMenu, pal);
                        if (cursorImages != null)
                            e.Graphics.DrawImage(cursorImages[0], 4, 71);
                    }
                    break;
            }
        }
        private void pictureBoxPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (textObjects == null)
                return;
            //
            foreach (TextObject textObject in textObjects)
            {
                if (e.X >= textObject.X && e.X < textObject.X + textObject.Width &&
                    e.Y >= textObject.Y && e.Y < textObject.Y + textObject.Height)
                {
                    mouseOverTextObject = textObject.Index;
                    pictureBoxPreview.Cursor = Cursors.Hand;
                    break;
                }
                else
                {
                    mouseOverTextObject = -1;
                    pictureBoxPreview.Cursor = Cursors.Arrow;
                }
            }
        }
        private void pictureBoxPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseOverTextObject >= 0 && mouseOverTextObject < Model.MenuTexts.Length)
                menusEditor.Index = mouseOverTextObject;
        }
        private void menuName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshMenu();
        }
        private void openPalettesBG_Click(object sender, EventArgs e)
        {
            if (bgPaletteEditor == null)
                LoadBGPaletteEditor();
            bgPaletteEditor.Show();
        }
        private void openPalettesFG_Click(object sender, EventArgs e)
        {
            if (fgPaletteEditor == null)
                LoadFGPaletteEditor();
            fgPaletteEditor.Show();
        }
        private void openGraphicsBG_Click(object sender, EventArgs e)
        {
            if (bgGraphicEditor == null)
                LoadBGGraphicEditor();
            bgGraphicEditor.Show();
        }
        private void openGraphicsFG_Click(object sender, EventArgs e)
        {
            if (fgGraphicEditor == null)
                LoadFGGraphicEditor();
            fgGraphicEditor.Show();
        }
        private void openPaletteCursors_Click(object sender, EventArgs e)
        {
            if (cursorsPaletteEditor == null)
                LoadCursorsPaletteEditor();
            cursorsPaletteEditor.Show();
        }
        private void openGraphicsCursors_Click(object sender, EventArgs e)
        {
            if (cursorsGraphicEditor == null)
                LoadCursorsGraphicEditor();
            cursorsGraphicEditor.Show();
        }
        private void openPaletteSpeakers_Click(object sender, EventArgs e)
        {
            if (speakersPaletteEditor == null)
                LoadSpeakersPaletteEditor();
            speakersPaletteEditor.Show();
        }
        private void openGraphicsSpeakers_Click(object sender, EventArgs e)
        {
            if (speakersGraphicEditor == null)
                LoadSpeakersGraphicEditor();
            speakersGraphicEditor.Show();
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip1.SourceControl == pictureBoxFG)
                Do.Export(fgImage, "menuFG.png");
            else if (contextMenuStrip1.SourceControl == pictureBoxBG)
                Do.Export(bgImage, "menuBG.png");
            else if (contextMenuStrip1.SourceControl == pictureBoxPreview)
                Do.Export(previewImage, "menuPreview.png");
        }
        private void importImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = LAZYSHELL.Properties.Settings.Default.LastRomPath;
            openFileDialog1.Title = "Import background image";
            openFileDialog1.Filter = "Image files (*.gif,*.jpg,*.png)|*.gif;*.jpg;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialog1.FileName == null)
                return;
            Bitmap import = new Bitmap(Image.FromFile(openFileDialog1.FileName));
            //
            if (contextMenuStrip1.SourceControl == pictureBoxBG || sender == importBGToolStripMenuItem)
                ImportBackground(import);
            else if (contextMenuStrip1.SourceControl == pictureBoxFG || sender == importFGToolStripMenuItem)
                ImportForeground(import);
        }
        private void cursorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cursorSpriteNum.SelectedIndex = cursorSprite.Sprite;
            cursorSequence.Value = cursorSprite.Sequence;
            cursorImage = null;
            pictureBoxPreview.Invalidate();
        }
        private void cursorSpriteNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            cursorSprite.Sprite = cursorSpriteNum.SelectedIndex;
            cursorImage = null;
            pictureBoxPreview.Invalidate();
        }
        private void cursorSequence_ValueChanged(object sender, EventArgs e)
        {
            cursorSprite.Sequence = (int)cursorSequence.Value;
            cursorImage = null;
            pictureBoxPreview.Invalidate();
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
        [Serializable()]
        public class CursorSprite
        {
            private int index;
            public int Sprite;
            public int Sequence;
            public CursorSprite(int index)
            {
                this.index = index;
                switch (index)
                {
                    case 0:
                        Sprite = Bits.GetShort(Model.ROM, 0x034757);
                        Sequence = Bits.GetShort(Model.ROM, 0x03475C);
                        break;
                    case 1:
                        Sprite = Bits.GetShort(Model.ROM, 0x03489A);
                        Sequence = Bits.GetShort(Model.ROM, 0x03489F);
                        break;
                    case 2:
                        Sprite = Bits.GetShort(Model.ROM, 0x034EE7);
                        Sequence = Bits.GetShort(Model.ROM, 0x034EEC);
                        break;
                    case 3:
                        Sprite = Bits.GetShort(Model.ROM, 0x0340AA);
                        Sequence = Bits.GetShort(Model.ROM, 0x0340AF);
                        break;
                    case 4:
                        Sprite = Bits.GetShort(Model.ROM, 0x03501E);
                        Sequence = Bits.GetShort(Model.ROM, 0x035021);
                        break;
                }
            }
            public void Assemble()
            {
                switch (index)
                {
                    case 0:
                        Bits.SetShort(Model.ROM, 0x034757, Sprite);
                        Bits.SetShort(Model.ROM, 0x03475C, Sequence);
                        break;
                    case 1:
                        Bits.SetShort(Model.ROM, 0x03489A, Sprite);
                        Bits.SetShort(Model.ROM, 0x03489F, Sequence);
                        break;
                    case 2:
                        Bits.SetShort(Model.ROM, 0x034EE7, Sprite);
                        Bits.SetShort(Model.ROM, 0x034EEC, Sequence);
                        break;
                    case 3:
                        Bits.SetShort(Model.ROM, 0x0340AA, Sprite);
                        Bits.SetShort(Model.ROM, 0x0340AF, Sequence);
                        break;
                    case 4:
                        Bits.SetShort(Model.ROM, 0x03501E, Sprite);
                        Bits.SetShort(Model.ROM, 0x035021, Sequence);
                        break;
                }
            }
        }
        private class TextObject
        {
            public Rectangle Rectangle;
            public int Index;
            public int X { get { return Rectangle.X; } }
            public int Y { get { return Rectangle.Y; } }
            public int Width { get { return Rectangle.Width; } }
            public int Height { get { return Rectangle.Height; } }
            public TextObject(int index, int x, int y)
            {
                this.Index = index;
                this.Rectangle = Model.MenuTexts[index].Rectangle(x, y);
            }
        }
    }
}
