using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.Undo;
using static System.Net.Mime.MediaTypeNames;
using static LAZYSHELL.NotesDB;

namespace LAZYSHELL
{
    public partial class MenusEditor : NewForm
    {
        #region Variables
        private delegate void Function();
        // main
        private Settings settings = Settings.Default;
        private TextHelperReduced textHelper = TextHelperReduced.Instance;
        public int Index { get { return menuTextName.SelectedIndex; } set { menuTextName.SelectedIndex = value; } }
        private MenuTexts menuText { get { return Model.MenuTexts[Index]; } set { Model.MenuTexts[Index] = value; } }
        //
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
                    return Model.GameSelectPaletteSet;
                else if (index < MenuType.Shop)
                    return Model.MenuBGPalette;
                else
                    return Model.ShopBGPalette;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    Model.GameSelectPaletteSet = value;
                else if (index < MenuType.Shop)
                    Model.MenuBGPalette = value;
                else
                    Model.ShopBGPalette = value;
            }
        }
        private byte[] bgGraphics
        {
            get
            {
                return Model.MenuBGGraphics;
            }
            set
            {
                Model.MenuBGGraphics = value;
            }
        }
        private PaletteSet fgPaletteSet
        {
            get
            {
                if (index == MenuType.GameSelect)
                    return Model.GameSelectPaletteSet;
                else if (index == MenuType.OverworldStarPieces)
                    return Model.OverworldStarPiecesMenuPalette;
                else
                    return Model.FontPaletteMenu;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    Model.GameSelectPaletteSet = value;
                else if (index == MenuType.OverworldStarPieces)
                    Model.OverworldStarPiecesMenuPalette = value;
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
                else if (index == MenuType.OverworldStarPieces)
                    return Model.OverworldStarPiecesMenuGraphics; //This pulls the same graphics as worldMapLogos!
                else
                    return Model.MenuFrameGraphics;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    Model.GameSelectGraphics = value;
                else if (index == MenuType.OverworldStarPieces)
                    Model.OverworldStarPiecesMenuGraphics = value;
                else
                    Model.MenuFrameGraphics = value;
            }
        }
        private byte[] graphicsDynamic
        {
            get
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    return bgGraphics;
                else
                    return fgGraphics;
            }
            set
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    bgGraphics = value;
                else
                    fgGraphics = value;
            }
        }
        private PaletteSet palettesDynamic
        {
            get
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    return bgPaletteSet;
                else
                    return fgPaletteSet;
            }
            set
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    bgPaletteSet = value;
                else
                    fgPaletteSet = value;
            }
        }
        private PaletteSet cursorPaletteSet { get { return Model.CursorPaletteSet; } set { Model.CursorPaletteSet = value; } }
        private PaletteSet starPiecesPalettes { get { return Model.OverworldStarPiecesMenuPalette; } set { Model.OverworldStarPiecesMenuPalette = value; } }
        //
        private MenuTileset bgTileset;

        private MenuTileset fgTilesetGameSelect;
        private MenuTileset fgTilesetStarPiecesOverworld;
        private MenuTileset fgTileset
        {
            get
            {
                if (index == MenuType.GameSelect)
                    return fgTilesetGameSelect;
                else
                    return fgTilesetStarPiecesOverworld;
            }
            set
            {
                if (index == MenuType.GameSelect)
                    fgTilesetGameSelect = value;
                else
                    fgTilesetStarPiecesOverworld = value;
            }
        }
        private MenuTileset tilesetDynamic
        {
            get
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    return bgTileset;
                else
                    return fgTileset;
            }
            set
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    bgTileset = value;
                else
                    fgTileset = value;
            }
        }


        //
        private Bitmap bgImage;
        private Bitmap fgImage;
        private Bitmap stereoImage;
        private Bitmap monoImage;
        private Bitmap cursorImage;
        private Bitmap previewImage;
        private bool editTilesetGameSelect;
        private bool editTilesetStarPiecesMenu;
        private Bitmap[] allyImages;
        private Bitmap[] cursorImages;
        private Bitmap[] starPiecesImages;
        public CursorSprite[] CursorSprites;
        private CursorSprite cursorSprite
        {
            get
            { return CursorSprites[cursorName.SelectedIndex]; }
            set
            { CursorSprites[cursorName.SelectedIndex] = value; }
        }
        public PictureBox Picture { get { return pictureBoxPreview; } set { pictureBoxPreview = value; } }
        //
        private List<TextObject> textObjects;
        private int mouseOverTextObject = -1;
        //
        // editors
        private MenusBox menusBox;
        private TileEditor tileEditor;
        private PaletteEditor bgPaletteEditor;
        private GraphicEditor bgGraphicEditor;
        private PaletteEditor fgPaletteEditor;
        private GraphicEditor fgGraphicEditor;
        private PaletteEditor cursorsPaletteEditor;
        private GraphicEditor cursorsGraphicEditor;
        private PaletteEditor speakersPaletteEditor;
        private GraphicEditor speakersGraphicEditor;
        private PaletteEditor starPiecesPaletteEditor;
        // buffers and stacks
        private Overlay overlay = new Overlay();
        private Bitmap selection;
        private CopyBuffer draggedTiles;
        private CopyBuffer copiedTiles;
        private CommandStack commandStack = new CommandStack(true);
        private int commandCount = 0;
        // mouse
        private int zoom = 1;
        private bool mouseEnter = false;
        private int mouseDownTile = 0;
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition;
        private Point mousePosition;
        private bool moving = false;
        private bool defloating = false;
        //
        #endregion
        //
        public MenusEditor()
        {
            //
            InitializeComponent();
            Do.AddShortcut(toolStrip1, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip1, Keys.F1, helpTips);
            new ToolTipLabel(this, null, helpTips);
            this.History = new History(this, menuName, null);
            //
            for (int i = 0; i < Model.MenuTexts.Length; i++)
                this.menuTextName.Items.Add(Model.MenuTexts[i].GetMenuString(textView.Checked));
            this.Index = 0;
            //
            //
            GC.Collect();
            //
            this.menusBox = new MenusBox(this);
            menusBox.TopLevel = false;
            menusBox.Dock = DockStyle.Fill;
            panel2.Controls.Add(menusBox);
            menusBox.Visible = true;
            //
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
            //
            //bgTileset = new Tileset(Model.MenuBGTileset, Model.MenuBGGraphics, bgPaletteSet, 16, 16, TilesetType.WorldMap);
            //fgTilesetGameSelect = new Tileset(Model.GameSelectTileset, Model.GameSelectGraphics, Model.GameSelectPaletteSet, 16, 16, TilesetType.WorldMap);
            //fgTilesetStarPiecesOverworld = new Tileset(Model.OverworldStarPiecesMenuTileset, Model.WorldMapGraphics, Model.OverworldStarPiecesMenuPalette, 16, 16, TilesetType.WorldMap);
            //
            bgTileset = new MenuTileset(bgPaletteSet, Model.MenuBGTileset, Model.MenuBGGraphics, TilesetType.MenuBackground);
            fgTilesetGameSelect = new MenuTileset(Model.GameSelectPaletteSet, Model.GameSelectTileset, Model.GameSelectGraphics, TilesetType.GameSelectMenu);
            fgTilesetStarPiecesOverworld = new MenuTileset(Model.OverworldStarPiecesMenuPalette, Model.OverworldStarPiecesMenuTileset, Model.OverworldStarPiecesMenuGraphics, TilesetType.StarPiecesOverworldMenu);
            SetAllyImages();
            SetStarPiecesImages();
            menuChooseBgOrFg.SelectedIndex = 0;
            SetBackgroundImage();
            SetForegroundImage();
            SetSpeakersImages();
            SetCursorImages();
            SetPreviewImage();
            SetTextObjects();
            //
            LoadTileEditor();
            this.Updating = false;
        }
        public void Reload()
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
            //
            SetAllyImages();
            SetStarPiecesImages();
            /*if (menuChooseBgOrFg.SelectedIndex == 1)
                SetBackgroundImage();
            else
                SetForegroundImage();
            */

            SetSpeakersImages();
            SetCursorImages();
            SetPreviewImage();
            SetTextObjects();
            BGGraphicUpdate();
            FGGraphicUpdate();
            LoadTileEditor();
            //
            //
            if (index == MenuType.GameSelect && menuChooseBgOrFg.SelectedIndex == 0)
                editTilesetGameSelect = true;
            else if (index == MenuType.OverworldStarPieces && menuChooseBgOrFg.SelectedIndex == 0)
                editTilesetStarPiecesMenu = true;
            //
            //
            GC.Collect();
            this.Updating = false;
        }
        private void RefreshMenuText()
        {
            this.Updating = true;
            this.menuTextBox.Text = menuText.GetMenuString(textView.Checked);
            this.toolStripSeparator2.Visible =
                this.toolStripLabel2.Visible =
                this.xCoord.Visible = menuText.Index >= 14 && menuText.Index <= 19;
            this.xCoord.Value = menuText.X;
            CalculateFreeSpace();
            this.Updating = false;
        }
        private int CalculateFreeSpace()
        {
            int used = 0;
            MenuTexts lastMenuText = null;
            foreach (MenuTexts menuText in Model.MenuTexts)
            {
                if (lastMenuText != null && menuText.Length != 0 && Bits.Compare(menuText.Text, lastMenuText.Text))
                    continue;
                lastMenuText = menuText;
                used += menuText.Length + 1;
            }
            int left = 0x700 - used;
            this.charactersLeft.Text = "(" + left.ToString() + " characters left)";
            this.charactersLeft.BackColor = left >= 0 ? SystemColors.Control : Color.Red;
            return left;
        }
        private void Assemble()
        {
            // Overworld Main Menu List
            foreach (MenuBox menu in Model.MenuBox)
                menu.Assemble();

            // Text
            int offset = 0;
            byte[] temp = new byte[0x700];
            MenuTexts lastMenuText = null;
            foreach (MenuTexts menuText in Model.MenuTexts)
            {
                if (lastMenuText != null && menuText.Length != 0 && Bits.Compare(menuText.Text, lastMenuText.Text))
                {
                    Bits.SetShort(Model.ROM, menuText.Index * 2 + 0x3EEF00, lastMenuText.Offset);
                    menuText.Offset = lastMenuText.Offset;
                    continue;
                }
                if (offset + menuText.Length + 1 >= 0x700)
                {
                    MessageBox.Show("Menu texts exceed allotted ROM space. Stopped saving at index " + menuText.Index + ".");
                    break;
                }
                menuText.Offset = offset;
                lastMenuText = menuText;
                //
                Bits.SetShort(Model.ROM, menuText.Index * 2 + 0x3EEF00, offset);
                Bits.SetChars(temp, offset, menuText.Text);
                offset += menuText.Length;
                temp[offset++] = 0;
                switch (menuText.Index)
                {
                    case 14: Bits.SetByteBits(Model.ROM, 0x03328E, (byte)(menuText.X * 2), 0x3F); break;
                    case 15: Bits.SetByteBits(Model.ROM, 0x03327E, (byte)(menuText.X * 2), 0x3F); break;
                    case 16: Bits.SetByteBits(Model.ROM, 0x033282, (byte)(menuText.X * 2), 0x3F); break;
                    case 17: Bits.SetByteBits(Model.ROM, 0x033286, (byte)(menuText.X * 2), 0x3F); break;
                    case 18: Bits.SetByteBits(Model.ROM, 0x03328A, (byte)(menuText.X * 2), 0x3F); break;
                    case 19: Bits.SetByteBits(Model.ROM, 0x03327A, (byte)(menuText.X * 2), 0x3F); break;
                }
            }
            Bits.SetBytes(Model.ROM, 0x3EF000, temp);
            //Bits.SetShort(Model.Data, 0x3EF600, 0x344F);

            // Music
            Model.ROM[0x03462D] = (byte)this.music.SelectedIndex;
            
            // Graphics
            Model.Compress(Model.GameSelectGraphics, 0x3E9A49, 0x2000, 0x3EB2CD - 0x3E9A49, "Game select graphics");
            if (editTilesetGameSelect)
            {
                fgTilesetGameSelect.Assemble();
                Model.Compress(fgTilesetGameSelect.Tileset_bytes, 0x3EB2CD, 0x800, 0x3EB50F - 0x3EB2CD, "Game select tileset");
            }
            Model.Compress(Model.GameSelectPalettes, 0x3EB50F, 0x200, 0x3EB624 - 0x3EB50F, "Game select palettes");
            Model.Compress(Model.GameSelectSpeakers, 0x3EB624, 0x600, 0x3EB94A - 0x3EB624, "Game select speakers");
            //
            for (int i = 0; i < CursorSprites.Length; i++)
                CursorSprites[i].Assemble();
            
            // Palettes

            Model.GameSelectPaletteSet.Assemble();
            Model.GameSelectBGPalette.Assemble();
            Model.FontPaletteMenu.Assemble(Model.MenuPalettes, 0);
            Model.MenuBGPalette.Assemble();
            Model.ShopBGPalette.Assemble();
            //Model.FramePalettes.Assemble(Model.MenuPalettes, 0x18);
            Model.OverworldStarPiecesMenuPalette.Assemble(Model.MenuPalettes, 0xC0);
            Model.CursorPaletteSet.Assemble(Model.MenuPalettes, 0x100);

            // Graphics
            offset = 0x4C;
            byte[] dst = new byte[0x2E81];
            // copy uncompressed world map logo graphics
            Bits.SetShort(dst, 0x00, offset);
            if (!Model.Compress(Model.OverworldStarPiecesMenuGraphics, dst, ref offset, 0x2E81, "World map logos, banners and star pieces menu")) goto Reset;
            //
            Bits.SetShort(dst, 0x02, offset);
            if (!Model.Compress(Model.MenuBGGraphics, dst, ref offset, 0x2E81, "BG graphics")) goto Reset;
            Bits.SetShort(dst, 0x04, offset);
            if (!Model.Compress(Model.MenuFrameGraphics, dst, ref offset, 0x2E81, "Frame graphics")) goto Reset;
            Bits.SetShort(dst, 0x06, offset);
            if (!Model.Compress(Model.MenuCursorGraphics, dst, ref offset, 0x2E81, "Cursor graphics")) goto Reset;

            Bits.SetShort(dst, 0x08, offset);
            bgTileset.Assemble(); //rebuilds tileset by adding tile indexes, so correct graphics are displayed in game
            if (!Model.Compress(bgTileset.Tileset_bytes, dst, ref offset, 0x2E81, "BG tileset")) goto Reset;

            Bits.SetShort(dst, 0x0A, offset);
            fgTilesetStarPiecesOverworld.Assemble(); //rebuilds tileset by adding tile indexes, so correct graphics are displayed in game
            if (!Model.Compress(fgTilesetStarPiecesOverworld.Tileset_bytes, dst, ref offset, 0x2E81, "Star Pieces Menu Tileset")) goto Reset;

            Bits.SetShort(dst, 0x0C, offset);
            if (!Model.Compress(Model.MenuPalettes, dst, ref offset, 0x2E81, "Menu palettes")) goto Reset;
            // set pointers (just the first 7 for menu data)
            Buffer.BlockCopy(dst, 0, Model.ROM, 0x3E0000, 0x0E); 
            // store compressed data (starting at start of data)
            Buffer.BlockCopy(dst, 0x4C, Model.ROM, 0x3E004C, dst.Length - 0x4C);

        Reset:
            this.Modified = false;
        }

        private void RefreshMenu()
        {
            toolStrip2.Visible = index == MenuType.GameSelect;
            music.Visible = index == MenuType.GameSelect;
            labelMusic.Visible = index == MenuType.GameSelect;
            //toolStripSeparator2.Visible = index == MenuType.GameSelect;
            importBGToolStripMenuItem.Text = index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? "Import Foreground" : "Import Frame";
            openPalettesFG.Text = index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? "Foreground Palettes" : "Frame Palette";
            openGraphicsFG.Text = index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? "Foreground Graphics" : "Frame Graphics";
            if (bgGraphicEditor != null)
                LoadBGGraphicEditor();
            if (fgGraphicEditor != null)
                LoadFGGraphicEditor();
            if (bgPaletteEditor != null)
                LoadBGPaletteEditor();
            if (fgPaletteEditor != null)
                LoadFGPaletteEditor();
            //
            bgTileset = new MenuTileset(bgPaletteSet, Model.MenuBGTileset, Model.MenuBGGraphics, TilesetType.MenuBackground);
            fgTilesetGameSelect = new MenuTileset(Model.GameSelectPaletteSet, Model.GameSelectTileset, Model.GameSelectGraphics, TilesetType.GameSelectMenu);
            fgTilesetStarPiecesOverworld = new MenuTileset(Model.OverworldStarPiecesMenuPalette, Model.OverworldStarPiecesMenuTileset, Model.OverworldStarPiecesMenuGraphics, TilesetType.StarPiecesOverworldMenu);
            //
            SetBackgroundImage();
            SetForegroundImage();
            SetPreviewImage();
            SetTextObjects();
            //
            mouseDownTile = 0;
            LoadTileEditor();
            TileUpdate();
            //
            if (index == MenuType.GameSelect || index == MenuType.OverworldStarPieces || menuChooseBgOrFg.SelectedIndex == 1)
                toolStrip3.Enabled = true;
            else
            {
                toolStrip3.Enabled = false;
            }
            buttonEditSelect.Checked = false;
            overlay.SelectTS.Clear();
            moving = false;
            selection = null;
            mouseOverTextObject = -1;
            pictureBoxEditor.Cursor = Cursors.Arrow;
            pictureBoxPreview.Cursor = Cursors.Arrow;
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
        private void SetStarPiecesImages()
        {
            starPiecesImages = new Bitmap[7];
            for (int i = 0, index = 82; i < starPiecesImages.Length; i++, index++)
            {
                Size size = new Size(0, 0);
                Sprite sprite = Model.Sprites[index];
                Animation animation = Model.Animations[sprite.AnimationPacket];
                Sequence sequence = animation.Sequences[0];
                int moldIndex = 0;
                if (sequence != null && sequence.Frames.Count >= 0)
                    moldIndex = sequence.Frames[0].Mold;
                int[] pixels = sprite.GetPixels(true, false, moldIndex, 0, false, false, ref size);
                starPiecesImages[i] = Do.PixelsToImage(pixels, size.Width, size.Height);
            }
        }
        private void SetBackgroundImage()
        {
            pictureBoxEditor.Size = new Size(256, 256);
            if (index == MenuType.GameSelect)
                bgImage = Model.GameBG;
            else if (index < MenuType.Shop)
                bgImage = Model.MenuBG;
            else
                bgImage = Model.ShopBG;

            pictureBoxEditor.Invalidate();
            pictureBoxPreview.Invalidate();
        }
        private void SetForegroundImage()
        {
            if (index == MenuType.GameSelect)
            {
                Tile[] tileset = new MenuTileset(fgPaletteSet, Model.GameSelectTileset, fgGraphics, TilesetType.GameSelectMenu).Tileset_tiles;
                int[] pixels = Do.TilesetToPixels(tileset, 16, 16, 0, false);
                fgImage = Do.PixelsToImage(pixels, 256, 256);
                pictureBoxEditor.Size = new Size(256, 256);
            }
            else if (index == MenuType.OverworldStarPieces)
            {
                Tile[] tileset = new MenuTileset(fgPaletteSet, Model.OverworldStarPiecesMenuTileset, fgGraphics, TilesetType.StarPiecesOverworldMenu).Tileset_tiles;
                int[] pixels = Do.TilesetToPixels(tileset, 16, 16, 0, false);
                fgImage = Do.PixelsToImage(pixels, 256, 256);
                pictureBoxEditor.Size = new Size(256, 256);
            }
            else if (menuChooseBgOrFg.SelectedIndex == 0)
            {
                fgImage = Do.PixelsToImage(
                    Do.DrawMenuFrame(new Size(5, 6), fgGraphics, fgPaletteSet.Palette), 40, 48);
                pictureBoxEditor.Size = new Size(40, 48);
            }

            pictureBoxEditor.Invalidate();
            pictureBoxPreview.Invalidate();
        }
        private void SetSpeakersImages()
        {
            int[] pixels = Do.GetPixelRegion(Model.GameSelectSpeakers, 0x20, Model.GameSelectPaletteSet.Palettes[14], 16, 8, 0, 7, 3, 0);
            stereoImage = Do.PixelsToImage(pixels, 56, 24);
            pixels = Do.GetPixelRegion(Model.GameSelectSpeakers, 0x20, Model.GameSelectPaletteSet.Palettes[15], 16, 0, 0, 7, 3, 0);
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
                default:
                    frames = new Rectangle[] // Overworld
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(136, 7, 14, 15),
                        new Rectangle(144, 127, 12, 11)
                    };
                    break;
                case MenuType.OverworldItem:
                    frames = new Rectangle[] // Overworld - Items
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(8, 151, 15, 8),
                        new Rectangle(8, 151, 15, 3),
                        new Rectangle(128, 7, 17, 25)
                    };
                    break;
                case MenuType.OverworldStatus:
                    frames = new Rectangle[] // Overworld - status
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(128, 7, 15, 25)
                    };
                    break;
                case MenuType.OverworldSpecial:
                    frames = new Rectangle[] // Overworld - special
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(128, 7, 15, 11),
                        new Rectangle(128, 103, 15, 3),
                        new Rectangle(128, 127, 15, 11)
                    };
                    break;
                case MenuType.OverworldEquip:
                    frames = new Rectangle[] // Overworld - equip
                    {
                        new Rectangle(0, 7, 17, 6),
                        new Rectangle(0, 55, 17, 6),
                        new Rectangle(0, 103, 17, 6),
                        new Rectangle(0, 151, 17, 8),
                        new Rectangle(136, 7, 17, 25)
                    };
                    break;
                case MenuType.OverworldSpecialItem:
                    frames = new Rectangle[] // Overworld - special item
                    {
                        new Rectangle(8, 39, 30, 14),
                        new Rectangle(64, 151, 16, 6)
                    };
                    break;
                case MenuType.OverworldSwitch:
                    frames = new Rectangle[] // Overworld - switch
                    {
                        new Rectangle(8, 7, 15, 6),
                        new Rectangle(8, 55, 15, 6),
                        new Rectangle(8, 103, 15, 6),
                        new Rectangle(128, 55, 15, 6),
                        new Rectangle(128, 103, 15, 6),
                        new Rectangle(136, 159, 13, 6)
                    };
                    break;
                case MenuType.Shop:
                    frames = new Rectangle[] // Shop
                    {
                        new Rectangle(8, 7, 15, 8),
                        new Rectangle(8, 79, 15, 3),
                        new Rectangle(128, 7, 15, 26)
                    };
                    break;
                case MenuType.ShopBuy:
                    frames = new Rectangle[] // Shop - buy
                    {
                        new Rectangle(8, 7, 15, 15),
                        new Rectangle(8, 127, 15, 8),
                        new Rectangle(128, 7, 15, 25)
                    };
                    break;
                case MenuType.ShopSellItems:
                    frames = new Rectangle[] // Shop - sell items
                    {
                        new Rectangle(8, 7, 15, 15),
                        new Rectangle(128, 7, 17, 25)
                    };
                    break;
                case MenuType.ShopSellWeapons:
                    frames = new Rectangle[] // Shop - sell weapons
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
                case MenuType.OverworldStarPieces:
                    textObjects.Add(new TextObject(6, 80, 16));
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
                        int[] s = new int[9];
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (menusBox.menus[i].SelectedIndex == 9)
                            {
                                if (i < 5)
                                    s[i] = 0;
                                else
                                    s[i] = menusBox.menus[i].SelectedIndex;
                            }
                            else
                                s[i] = menusBox.menus[i].SelectedIndex;

                        }
                        textObjects.Add(new TextObject(s[0], 143, 12));
                        textObjects.Add(new TextObject(s[1], 143, 24));
                        textObjects.Add(new TextObject(s[2], 143, 36));
                        textObjects.Add(new TextObject(s[3], 143, 48));
                        textObjects.Add(new TextObject(s[4], 143, 60));
                        textObjects.Add(new TextObject(s[5], 143, 72));
                        textObjects.Add(new TextObject(s[6], 143, 84));
                        textObjects.Add(new TextObject(s[7], 143, 96));
                        textObjects.Add(new TextObject(s[8], 143, 108));
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

        #region Functions

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
                    bgGraphics, bgGraphics.Length, 0, bgPaletteSet, 0, 0x20);
                bgGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                bgGraphicEditor.Reload(new Function(BGGraphicUpdate),
                    bgGraphics, bgGraphics.Length, 0, bgPaletteSet, 0, 0x20);
        }
        private void LoadFGPaletteEditor()
        {
            if (fgPaletteEditor == null)
            {
                if (index == MenuType.GameSelect)
                    fgPaletteEditor = new PaletteEditor(new Function(FGPaletteUpdate), fgPaletteSet, 16, 1, 5);
                else
                    fgPaletteEditor = new PaletteEditor(new Function(FGPaletteUpdate), fgPaletteSet, 2, 0, 2);
                fgPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
            {
                if (index == MenuType.GameSelect)
                    fgPaletteEditor.Reload(new Function(FGPaletteUpdate), fgPaletteSet, 16, 1, 5);
                else
                    fgPaletteEditor.Reload(new Function(FGPaletteUpdate), fgPaletteSet, 2, 0, 2);
            }
        }
        private void LoadFGGraphicEditor()
        {
            if (fgGraphicEditor == null)
            {
                fgGraphicEditor = new GraphicEditor(new Function(FGGraphicUpdate),
                    fgGraphics, fgGraphics.Length, 0,
                    index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? fgPaletteSet : Model.FramePalettes,
                    0,
                    (byte)(index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? 0x20 : 0x10));
                fgGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                fgGraphicEditor.Reload(new Function(FGGraphicUpdate),
                    fgGraphics, fgGraphics.Length, 0,
                    index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? fgPaletteSet : Model.FramePalettes,
                    0,
                    (byte)(index == MenuType.GameSelect || index == MenuType.OverworldStarPieces ? 0x20 : 0x10));
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
                speakersPaletteEditor = new PaletteEditor(new Function(SpeakersPaletteUpdate), Model.GameSelectPaletteSet, 16, 14, 2);
                speakersPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                speakersPaletteEditor.Reload(new Function(SpeakersPaletteUpdate), Model.GameSelectPaletteSet, 16, 14, 2);
        }
        private void LoadSpeakersGraphicEditor()
        {
            if (speakersGraphicEditor == null)
            {
                speakersGraphicEditor = new GraphicEditor(new Function(SpeakersGraphicUpdate),
                    Model.GameSelectSpeakers, Model.GameSelectSpeakers.Length, 0, Model.GameSelectPaletteSet, 14, 0x20);
                speakersGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                speakersGraphicEditor.Reload(new Function(SpeakersGraphicUpdate),
                    Model.GameSelectSpeakers, Model.GameSelectSpeakers.Length, 0, Model.GameSelectPaletteSet, 14, 0x20);
        }
        private void BGPaletteUpdate()
        {
            Model.MenuBG = null;
            Model.ShopBG = null;
            Model.GameBG = null;
            SetBackgroundImage();
            SetPreviewImage();
            LoadBGGraphicEditor();
            LoadTileEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void BGGraphicUpdate()
        {
            Model.MenuBG = null;
            Model.ShopBG = null;
            Model.GameBG = null;
            SetBackgroundImage();
            SetPreviewImage();
            LoadTileEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void FGPaletteUpdate()
        {
            SetForegroundImage();
            SetPreviewImage();
            LoadFGGraphicEditor();
            LoadTileEditor();
            this.Modified = true;   // b/c switching colors won't modify
        }
        private void FGGraphicUpdate()
        {
            SetForegroundImage();
            SetPreviewImage();
            LoadTileEditor();
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
        private void LoadTileEditor()
        {
            if (tileEditor == null)
            {
                tileEditor = new TileEditor(new Function(TileUpdate),
                this.tilesetDynamic.Tileset_tiles[mouseDownTile],
                graphicsDynamic, palettesDynamic, 0x20, false);
                tileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tileEditor.Reload(new Function(TileUpdate),
                this.tilesetDynamic.Tileset_tiles[mouseDownTile],
                graphicsDynamic, palettesDynamic, 0x20);
        }
        private void TileUpdate()
        {
            //bgTileset = new Tileset(Model.MenuBGTileset, Model.MenuBGGraphics, bgPaletteSet, 16, 16, TilesetType.WorldMap);

            tilesetDynamic.Tileset.DrawTileset(tilesetDynamic.Tileset_tiles, tilesetDynamic.Tileset_bytes);
            Reload();
        }
        //
        private void ImportBackground(Bitmap import)
        {
            if (import.Width > 256 || import.Height > 256)
            {
                MessageBox.Show(
                    "The dimensions of the imported image must be no larger than 256x256.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                editTilesetGameSelect = true;
            }
            else if (index == MenuType.OverworldStarPieces)
            {
                if (import.Width > 256 || import.Height > 256)
                {
                    MessageBox.Show(
                        "The dimensions of the imported image must be no larger than 256 x 256.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //
                byte[] graphics = new byte[0x8000];
                int[] pixels = Do.ImageToPixels(import, new Size(256, 256), new Rectangle(0, 0, 256, 256));
                int[] palette = Do.ReduceColorDepth(pixels, 16, 0);
                for (int i = 0; i < palette.Length; i++)
                {
                    fgPaletteSet.Reds[i] = Color.FromArgb(palette[i]).R;
                    fgPaletteSet.Greens[i] = Color.FromArgb(palette[i]).G;
                    fgPaletteSet.Blues[i] = Color.FromArgb(palette[i]).B;
                }
                Do.PixelsToBPP(pixels, graphics, new Size(256 / 8, 256 / 8), palette, 0x20);
                //
                byte[] tileset = new byte[0x800];
                byte[] temp = new byte[graphics.Length]; graphics.CopyTo(temp, 0);
                int size = Do.CopyToTileset(graphics, tileset, palette, 0, true, false, 0x20, 2, new Size(256, 256), 0);
                //
                Buffer.BlockCopy(tileset, 0, Model.OverworldStarPiecesMenuTileset, 0, 0x800);
                Buffer.BlockCopy(graphics, 0, Model.OverworldStarPiecesMenuGraphics, 0, 0x2000);
                fgTilesetStarPiecesOverworld.Tileset_bytes = tileset;
                if (size > 8192)
                    MessageBox.Show("Not enough space to store the necessary amount of SNES graphics data for the imported images. The total required space (" +
                        size + " bytes) for the new SNES graphics data exceeds 8192 bytes.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                editTilesetStarPiecesMenu = true;
            }
            else
            {
                if (import.Width != 40 || import.Height != 48)
                {
                    MessageBox.Show(
                        "The dimensions of the imported image must be exactly 40 x 48.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //
                byte[] graphics = new byte[0x1E0];
                int[] pixels = Do.ImageToPixels(import, new Size(40, 48), new Rectangle(0, 0, 40, 48));
                int[] palette = Do.ReduceColorDepth(pixels, 4, 0);
                Do.PixelsToBPP(pixels, graphics, new Size(5, 6), palette, 0x10);
                for (int i = 0; i < palette.Length; i++)
                {
                    Model.FontPaletteMenu.Reds[i + 12] = Color.FromArgb(palette[i]).R;
                    Model.FontPaletteMenu.Greens[i + 12] = Color.FromArgb(palette[i]).G;
                    Model.FontPaletteMenu.Blues[i + 12] = Color.FromArgb(palette[i]).B;
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
            RefreshMenu();
            this.Modified = true;
        }
        public void CloseEditors()
        {
            tileEditor.Close();
            tileEditor.Dispose();
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
            if (starPiecesPaletteEditor != null)
            {
                starPiecesPaletteEditor.Close();
                starPiecesPaletteEditor.Dispose();
            }
        }

        // Editing
        private void DrawHoverBox(Graphics g)
        {
            Rectangle r = new Rectangle(mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 16 * zoom, 16 * zoom);
            g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
        }
        private void Copy()
        {
            if (overlay.SelectTS.Empty)
                return;
            if (draggedTiles != null)
            {
                this.copiedTiles = draggedTiles;
                return;
            }
            // make the copy
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            this.copiedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] copiedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tilesetDynamic.Tileset_tiles[(y + y_) * 16 + x + x_].Copy();
                }
            }
            this.copiedTiles.Tiles = copiedTiles;
        }
        /// <summary>
        /// Start dragging a selection.
        /// </summary>
        private void Drag()
        {
            if (overlay.SelectTS.Empty)
                return;
            // make the copy
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            this.draggedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] draggedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    draggedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tilesetDynamic.Tileset_tiles[(y + y_) * 16 + x + x_].Copy();
                }
            }
            this.draggedTiles.Tiles = draggedTiles;
            selection = new Bitmap(this.draggedTiles.Image);
            Delete();
        }
        private void Cut()
        {
            if (overlay.SelectTS.Empty || overlay.SelectTS.Size == new Size(0, 0))
                return;
            Copy();
            Delete();
            if (commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            moving = true;
            // now dragging a new selection
            draggedTiles = buffer;
            selection = buffer.Image;
            overlay.SelectTS.Refresh(16, location, buffer.Size, pictureBoxEditor);
            pictureBoxEditor.Invalidate();
            defloating = false;
        }
        /// <summary>
        /// "Cements" either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        private void Defloat(CopyBuffer buffer)
        {
            byte[] oldTileset = Bits.Copy(tilesetDynamic.Tileset_bytes);
            //
            selection = null;
            int x_ = overlay.SelectTS.X / 16;
            int y_ = overlay.SelectTS.Y / 16;
            for (int y = 0; y < buffer.Height / 16; y++)
            {
                for (int x = 0; x < buffer.Width / 16; x++)
                {
                    if (y + y_ < 0 || y + y_ >= 16 ||
                        x + x_ < 0 || x + x_ >= 16)
                        continue;
                    Tile tile = buffer.Tiles[y * (buffer.Width / 16) + x];
                    tilesetDynamic.Tileset_tiles[(y + y_) * 16 + x + x_] = tile.Copy();
                    tilesetDynamic.Tileset_tiles[(y + y_) * 16 + x + x_].Index = (y + y_) * 16 + x + x_;
                }
            }
            tilesetDynamic.Tileset.DrawTileset(tilesetDynamic.Tileset_tiles, tilesetDynamic.Tileset_bytes);
            commandStack.Push(commandCount + 1);
            commandCount = 0;
            Reload();
            defloating = true;
            //
            commandStack.Push(new TilesetCommand(tilesetDynamic.Tileset, oldTileset, graphicsDynamic, 0x20, menuTextName));
        }
        private void Defloat()
        {
            if (copiedTiles != null && !defloating)
                Defloat(copiedTiles);
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            moving = false;
            overlay.SelectTS.Clear();
            Cursor.Position = Cursor.Position;
        }
        private void Delete()
        {
            if (overlay.SelectTS.Empty)
                return;
            byte[] oldTileset = Bits.Copy(tilesetDynamic.Tileset_bytes);
            //
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16 && y + y_ < 0x100; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16 && x + x_ < 0x100; x++)
                    tilesetDynamic.Tileset_tiles[(y + y_) * 16 + x + x_].Clear();
            }
            tilesetDynamic.Tileset.DrawTileset(tilesetDynamic.Tileset_tiles, tilesetDynamic.Tileset_bytes);
            Reload();
            //
            commandStack.Push(new TilesetCommand(tilesetDynamic.Tileset, oldTileset, graphicsDynamic, 0x20, menuTextName));
            commandCount++;
        }
        private void Flip(string type)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS.Empty)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            CopyBuffer buffer = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] copiedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tilesetDynamic.Tileset_tiles[(y + y_) * 16 + x + x_].Copy();
                }
            }
            if (type == "mirror")
                Do.FlipHorizontal(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16);
            else if (type == "invert")
                Do.FlipVertical(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16);
            buffer.Tiles = copiedTiles;
            Defloat(buffer);
            tilesetDynamic.Tileset.DrawTileset(tilesetDynamic.Tileset_tiles, tilesetDynamic.Tileset_bytes);
            Reload();
        }


        #endregion
        //
        //
        //
        //
        #region Event Handlers
        private void pictureBoxEditor_Paint(object sender, PaintEventArgs e)
        {
            if (bgImage == null) return;
            if (fgImage == null) return;
            //
            if (!showBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(bgPaletteSet.Palette[0])), new Rectangle(new Point(0, 0), pictureBoxEditor.Size));
            
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            Rectangle rdst = new Rectangle(0, 0, 256, 256);
            if (menuChooseBgOrFg.SelectedIndex == 1 && bgImage != null)
                e.Graphics.DrawImage(bgImage, rdst, 0, 0, 256, 256, GraphicsUnit.Pixel);
            else if (menuChooseBgOrFg.SelectedIndex == 0 && fgImage != null)
                e.Graphics.DrawImage(fgImage, rdst, 0, 0, 256, 256, GraphicsUnit.Pixel);

            if (moving && selection != null)
            {
                Rectangle rsrc = new Rectangle(0, 0, overlay.SelectTS.Width, overlay.SelectTS.Height);
                rdst = new Rectangle(
                    overlay.SelectTS.X * zoom, overlay.SelectTS.Y * zoom,
                    rsrc.Width * zoom, rsrc.Height * zoom);
                e.Graphics.DrawImage(new Bitmap(selection), rdst, rsrc, GraphicsUnit.Pixel);
                Do.DrawString(e.Graphics, new Point(rdst.X, rdst.Y + rdst.Height),
                    "click/drag", Color.White, Color.Black, new Font("Tahoma", 6.75F, FontStyle.Bold));
                //
                if (index == MenuType.GameSelect && menuChooseBgOrFg.SelectedIndex == 0)
                    editTilesetGameSelect = true;
                else if (index == MenuType.OverworldStarPieces && menuChooseBgOrFg.SelectedIndex == 0)
                    editTilesetStarPiecesMenu = true;
            }
            float[][] matrixItems ={
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, 0.50F, 0},
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix cm = new ColorMatrix(matrixItems);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            if (mouseEnter)
                DrawHoverBox(e.Graphics);
            if (buttonToggleGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxEditor.Size, new Size(16, 16), 1, true);
            if (overlay.SelectTS != null)
                overlay.SelectTS.DrawSelectionBox(e.Graphics, 1);
        }
        private void pictureBoxPreview_Paint(object sender, PaintEventArgs e)
        {
            if (previewImage != null)
            {
                if (!showBG.Checked)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(bgPaletteSet.Palette[0])), new Rectangle(new Point(0, 0), pictureBoxPreview.Size));
                if (index != MenuType.GameSelect && index != MenuType.OverworldStarPieces)
                    e.Graphics.DrawImage(previewImage, 0, 0);
            }
            int[] pal = Model.FontPaletteMenu.Palette;
            MenuTextPreview pre = new MenuTextPreview();
            //
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
                case MenuType.OverworldStarPieces: // Overworld Star Piece Menu
                    if (bgImage != null)
                        e.Graphics.DrawImage(bgImage, 0, 0);
                    if (fgImage != null)
                        e.Graphics.DrawImage(fgImage, 0, 0);

                    Do.DrawText(Model.MenuTexts[6].ToString(), 80, 16, e.Graphics, pre, Model.FontDialogue, pal);

                    if (starPiecesImages != null)
                    {
                        e.Graphics.DrawImage(starPiecesImages[0], 128 - 128, 32 - 96 - 1);
                        e.Graphics.DrawImage(starPiecesImages[4], 184 - 128, 56 - 96 - 1);
                        e.Graphics.DrawImage(starPiecesImages[3], 184 - 128, 104 - 96 - 1);
                        e.Graphics.DrawImage(starPiecesImages[1], 128 - 128, 128 - 96 - 1);
                        e.Graphics.DrawImage(starPiecesImages[5], 72 - 128, 104 - 96 - 1);
                        e.Graphics.DrawImage(starPiecesImages[2], 72 - 128, 56 - 96 - 1);
                        e.Graphics.DrawImage(starPiecesImages[6], 128 - 128, 80 - 96 - 1);
                    }
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
                        string[] s = new string[9];
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (menusBox.menus[i].SelectedIndex == 9)
                            {
                                if ( i < 5 )
                                    s[i] = " " + Model.MenuTexts[0].ToString();
                                else
                                    s[i] = Model.MenuTexts[i].ToString();
                            }
                            else
                                s[i] = menusBox.menus[i].Text;

                        }
                        Do.DrawText(s[0], 143, 12, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[1], 143, 24, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[2], 143, 36, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[3], 143, 48, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[4], 143, 60, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[5], 143, 72, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[6], 143, 84, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[7], 143, 96, e.Graphics, pre, Model.FontMenu, pal);
                        Do.DrawText(s[8], 143, 108, e.Graphics, pre, Model.FontMenu, pal);
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
                Index = mouseOverTextObject;
        }
        private void menuName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshMenu();
        }
        private void menuChooseBgOrFg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshMenu();
            if (menuChooseBgOrFg.SelectedIndex == 1)
                SetBackgroundImage();
            else
                SetForegroundImage();
        }
        // drawing buttons
        #region drawingButtons
        private void pictureBoxEditor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)

        {
            switch (e.KeyData)
            {
                case Keys.B: showBG.PerformClick(); break;
                case Keys.G: buttonToggleGrid.PerformClick(); break;
                case Keys.S: buttonEditSelect.PerformClick(); break;
                case Keys.Control | Keys.V: buttonEditPaste.PerformClick(); break;
                case Keys.Control | Keys.C: buttonEditCopy.PerformClick(); break;
                case Keys.Delete: buttonEditDelete.PerformClick(); break;
                case Keys.Control | Keys.X: buttonEditCut.PerformClick(); break;
                case Keys.Control | Keys.D:
                    if (draggedTiles != null)
                    {
                        Defloat(draggedTiles);
                    }
                    else
                    {
                        overlay.SelectTS.Clear();
                        Reload();
                    }
                    break;
                case Keys.Control | Keys.A:
                    overlay.SelectTS.Refresh(16, 0, 0, 256, 256, pictureBoxEditor);
                    Reload();
                    break;
                case Keys.Control | Keys.Z: buttonEditUndo.PerformClick(); break;
                case Keys.Control | Keys.Y: buttonEditRedo.PerformClick(); break;
            }
        }
        private void pictureBoxEditor_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            pictureBoxEditor.Focus();
            pictureBoxEditor.Invalidate();
        }
        private void pictureBoxEditor_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            pictureBoxEditor.Invalidate();
        }
        private void pictureBoxEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            mouseDownObject = null;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBoxEditor.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBoxEditor.Height));
            pictureBoxEditor.Focus();
            if (buttonEditSelect.Checked)
            {
                // if moving an object and outside of it, paste it
                if (moving && mouseOverObject != "selection")
                {
                    // if copied tiles were pasted and not dragging a non-copied selection
                    if (copiedTiles != null && draggedTiles == null)
                        Defloat(copiedTiles);
                    if (draggedTiles != null)
                    {
                        Defloat(draggedTiles);
                        draggedTiles = null;
                    }
                    selection = null;
                    moving = false;
                }
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseOverObject == null)
                    overlay.SelectTS.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, pictureBoxEditor);
                // if moving a current selection
                if (e.Button == MouseButtons.Left && mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.SelectTS.MousePosition(x, y);
                    if (!moving)    // only do this if the current selection has not been initially moved
                    {
                        moving = true;
                        Drag();
                    }
                }
            }
            mouseDownTile = y / 16 * 16 + (x / 16);
            LoadTileEditor();
        }
        private void pictureBoxEditor_MouseMove(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBoxEditor.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBoxEditor.Height));
            mouseOverObject = null;
            mousePosition = new Point(x, y);
            if (buttonEditSelect.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.SelectTS != null)
                    overlay.SelectTS.Final = new Point(
                        Math.Min(x + 16, pictureBoxEditor.Width),
                        Math.Min(y + 16, pictureBoxEditor.Height));
                // if dragging the current selection
                if (e.Button == MouseButtons.Left && mouseDownObject == "selection")
                    overlay.SelectTS.Location = new Point(
                        x / 16 * 16 - mouseDownPosition.X,
                        y / 16 * 16 - mouseDownPosition.Y);
                // check if over selection
                if (e.Button == MouseButtons.None && overlay.SelectTS != null && overlay.SelectTS.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxEditor.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxEditor.Cursor = Cursors.Cross;
            }
            pictureBoxEditor.Invalidate();
        }
        private void pictureBoxEditor_MouseUp(object sender, MouseEventArgs e)
        {

        }


        //
        //
        //
        private void buttonEditDelete_Click(object sender, EventArgs e)
        {
            if (!moving)
            {
                Delete();
            }
            else
            {
                moving = false;
                draggedTiles = null;
                Reload();
            }
            if (!moving && commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void buttonEditCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void buttonEditCut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void buttonEditPaste_Click(object sender, EventArgs e)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);

            Paste(new Point(16, 16), copiedTiles);
        }
        private void buttonEditUndo_Click(object sender, EventArgs e)
        {
            commandStack.UndoCommand();
            Reload();
        }
        private void buttonEditRedo_Click(object sender, EventArgs e)
        {
            commandStack.RedoCommand();
            Reload();
        }
        private void buttonEditSelect_Click(object sender, EventArgs e)
        {
            if (buttonEditSelect.Checked)
            {
                this.pictureBoxEditor.Cursor = Cursors.Cross;
            }
            else
            {
                this.pictureBoxEditor.Cursor = Cursors.Arrow;
            }
            Defloat();
            pictureBoxEditor.Invalidate();
        }
        // context menu strip
        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip("mirror");
        }
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip("invert");
        }
        #endregion


        // open editors
        #region openEditors
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
        #endregion

        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip1.SourceControl == pictureBoxEditor)
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    Do.Export(bgImage, "menuBG.png");
                else if (menuChooseBgOrFg.SelectedIndex == 0)
                    Do.Export(fgImage, "menuFG.png");
            }
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
            Bitmap import = new Bitmap(System.Drawing.Image.FromFile(openFileDialog1.FileName));
            //
            if (contextMenuStrip1.SourceControl == pictureBoxEditor || sender == importBGToolStripMenuItem)
            {
                if (menuChooseBgOrFg.SelectedIndex == 1)
                    ImportBackground(import);
                else if (menuChooseBgOrFg.SelectedIndex == 0)
                    ImportForeground(import);
            }
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
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void MenusEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(this.Modified || menusBox.Modified || this.Updating))
                goto Close;
            DialogResult result = MessageBox.Show(
                "Menus have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.MenuFrameGraphics = null;
                Model.FontPaletteMenu = null;
                Model.MenuBGGraphics = null;
                Model.MenuBGPalette = null;
                Model.ShopBGPalette = null;
                Model.MenuBGTileset = null;
                Model.MenuCursorGraphics = null;
                //
                Model.GameSelectGraphics = null;
                Model.GameSelectPalettes = null;
                Model.GameSelectPaletteSet = null;
                Model.GameSelectTileset = null;
                //
                Model.MenuTexts = null;
                //
                Model.OverworldStarPiecesMenuGraphics = null;
                Model.OverworldStarPiecesMenuPalette = null;
                Model.OverworldStarPiecesMenuTileset = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            this.CloseEditors();
        }
        private void menuTextNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            menuTextName.SelectedIndex = (int)menuTextNum.Value;
            this.Updating = false;
            RefreshMenuText();
        }
        private void menuTextName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            menuTextNum.Value = menuTextName.SelectedIndex;
            this.Updating = false;
            RefreshMenuText();
        }
        private void menuTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            //
            char[] text = menuTextBox.Text.ToCharArray();
            char[] swap;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    int tempSel = menuTextBox.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    menuTextBox.Text = new string(swap);
                    text = menuTextBox.Text.ToCharArray();
                    i += 2;
                    menuTextBox.SelectionStart = tempSel + 2;
                }
            }
            if (textHelper.VerifySymbols(this.menuTextBox.Text.ToCharArray(), !textView.Checked))
            {
                menuText.SetMenuString(menuTextBox.Text, textView.Checked);
                menuText.Error = textHelper.Error;
                this.Updating = true;
                int index = this.Index;
                menuTextName.Items.RemoveAt(index);
                menuTextName.Items.Insert(index, menuTextBox.Text);
                menuTextName.Text = menuTextBox.Text;
                menuTextName.Invalidate();
                this.Index = index;
                this.Updating = false;
            }
            CalculateFreeSpace();
            this.Picture.Invalidate();
            menusBox.RefreshMenus(menuTextName.Text, this.Index);
        }
        private void textView_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void xCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            menuText.X = (int)xCoord.Value;
            this.SetTextObjects();
            this.Picture.Invalidate();
        }

        private void openMenus_Click(object sender, EventArgs e)
        {
            panel1.Visible = openMenus.Checked;
            this.toolStripText.Dock = panel1.Visible ? DockStyle.Bottom : DockStyle.Top;
            this.toolStrip1.Dock = DockStyle.Top;
        }
        private void openText_Click(object sender, EventArgs e)
        {
            toolStripText.Visible = openText.Checked;
            this.toolStripText.Dock = panel1.Visible ? DockStyle.Bottom : DockStyle.Top;
            this.toolStrip1.Dock = DockStyle.Top;
        }
        private void openOverworldMenuList_Click(object sender, EventArgs e)
        {
            panel2.Visible = openOverworldMenuList.Checked;
        }
        private void showBG_Click(object sender, EventArgs e)
        {
            pictureBoxEditor.Invalidate();
            pictureBoxPreview.Invalidate();
        }
        private void buttonToggleGrid_Click(object sender, EventArgs e)
        {
            pictureBoxEditor.Invalidate();
            pictureBoxPreview.Invalidate();
        }
        private void openTileEditor_Click(object sender, EventArgs e)
        {
            tileEditor.Visible = true;
        }
        #region Resets
        // Text Reset
        private void resetTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Menu Text.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuTexts = null;
            int idx = this.Index;
            this.menuTextName.Items.Clear();
            for (int i = 0; i < Model.MenuTexts.Length; i++)
                this.menuTextName.Items.Add(Model.MenuTexts[i].GetMenuString(textView.Checked));
            this.Index = idx;
            //
            Model.MenuBoxNames = null;
            menusBox.InitializeStrings();
            //
            RefreshMenuText();
        }

        // Overworld Menu List Reset
        private void resetOverworldMenuListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Overworld Menu List.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuBox = null;
            menusBox.InitializeStrings();
        }
        // Tilesets Reset
        private void gameSelectTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Game Select Tilemap.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectTileset = null;
            RefreshMenu();
        }

        private void backgroundTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Background Tilemap.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuBGTileset = null;
            RefreshMenu();

        }

        private void starPiecesTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Game Select Tilemap.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.OverworldStarPiecesMenuTileset = null;
            RefreshMenu();
        }
        private void resetAllTilemapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to ALL tilemaps.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectTileset = null;
            Model.MenuBGTileset = null;
            Model.OverworldStarPiecesMenuTileset = null;
            Reload();
        }

        // Graphics Reset

        private void resetAllGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to ALL graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectGraphics = null;
            Model.MenuBGGraphics = null;
            Model.MenuFrameGraphics = null;
            Model.GameSelectSpeakers = null;
            Model.OverworldStarPiecesMenuGraphics = null;
            Model.MenuCursorGraphics = null;
            Reload();
        }


        private void gameSelectGraphicsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Game Select Graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectGraphics = null;
            Reload();
        }
        private void backgroundGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the Background Graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuBGGraphics = null;
            Reload();
        }
        private void frameGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Frame Graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuFrameGraphics = null;
            Reload();
        }
        private void starPiecesGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Star Pieces Menu Graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.OverworldStarPiecesMenuGraphics = null;
            Reload();
        }
        private void monoStereoGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Star Pieces Menu Graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectSpeakers = null;
            Reload();
        }
        private void cursorsGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Menu Cursors Graphics.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuCursorGraphics = null;
            Reload();
        }

        // Palette resets
        private void resetAllPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to ALL palettes.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectPaletteSet = null;
            Model.MenuBGPalette = null;
            Model.ShopBGPalette = null;
            Model.GameSelectBGPalette = null;
            Model.FontPaletteMenu = null;
            Model.OverworldStarPiecesMenuPalette = null;
            Model.CursorPaletteSet = null;
            Reload();
        }
        private void gameSelectPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Game Select Palette.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.GameSelectPaletteSet = null;
            Reload();
        }
        private void backgroundPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to all Background Palettes.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.MenuBGPalette = null;
            Model.ShopBGPalette = null;
            Model.GameSelectBGPalette = null;
            Reload();
        }
        private void framePaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Menu Palette.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.FontPaletteMenu = null;
            Reload();
        }
        private void starPiecesPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Star Pieces Menu Palette.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.OverworldStarPiecesMenuPalette = null;
            Reload();
        }
        private void cursorsPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to Menu Cursor Palette.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.CursorPaletteSet = null;
            Reload();
        }
        #endregion

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
