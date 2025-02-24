using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Coordinates : NewForm
    {
        #region Variables
        //
        private Settings settings = Settings.Default;
        private Character[] characters { get { return Model.Characters; } set { Model.Characters = value; } }
        private Character character { get { return parentEditor.Characters[parentEditor.Index]; } set { parentEditor.Characters[parentEditor.Index] = value; } }
        private int index { get { return parentEditor.Index; } set { parentEditor.Index = value; } }
        private int currentSprite { get { return cursorSpriteChoice.SelectedIndex; } set { cursorSpriteChoice.SelectedIndex = value; } }
        private int CursorX { get { return (int)character.AllyCoordinates[index].CursorX; } set { character.AllyCoordinates[index].CursorX = (byte)value; } }
        private int CursorY { get { return (int)character.AllyCoordinates[index].CursorY; } set { character.AllyCoordinates[index].CursorY = (byte)value; } }
        private int SpriteABXY_Y { get { return (int)character.AllyCoordinates[index].SpriteABXY_Y; } set { character.AllyCoordinates[index].SpriteABXY_Y = (byte)value; } }
        //
        private int CursorX_Scarecrow { get { return (int)character.AllyCoordinates[index].CursorX_Scarecrow; } set { character.AllyCoordinates[index].CursorX_Scarecrow = (byte)value; } }
        private int CursorY_Scarecrow { get { return (int)character.AllyCoordinates[index].CursorY_Scarecrow; } set { character.AllyCoordinates[index].CursorY_Scarecrow = (byte)value; } }
        private int SpriteABXY_Y_Scarecrow { get { return (int)character.AllyCoordinates[index].SpriteABXY_Y_Scarecrow; } set { character.AllyCoordinates[index].SpriteABXY_Y_Scarecrow = (byte)value; } }
        //
        private int ABXY_Height, ABXY_Width;
        private bool waitBothCoords = false;
        private bool overTarget = false;

        private int[][] characterImages;

        private Bitmap CharacterPaintBoxImage;
        private Bitmap CharacterImage
        {
            get
            {
                int[] pixels, palette, cursor;
                int _X, _Y, zeroCoord;
                Size imgSize = new Size(0, 0);
                
                if (currentSprite == 0)
                {
                    if (scarecrowButton.Checked)
                        pixels = (int[])characterImages[5].Clone();
                    else
                        pixels = (int[])characterImages[index].Clone();

                    Sprite sprite = Model.Sprites[512];
                    int moldIndex = 2;
                    cursor = sprite.GetPixels(true, false, moldIndex, 0, false, true, ref imgSize);

                    zeroCoord = -128;
                    _X = 255; 
                    if (scarecrowButton.Checked)
                        _Y = SpriteABXY_Y_Scarecrow;
                    else
                        _Y = SpriteABXY_Y;

                    ABXY_Width = imgSize.Width;
                    ABXY_Height = imgSize.Height;

                    for (int y = zeroCoord + (imgSize.Height / 2) + _Y, n = 0; n < imgSize.Height; y++, n++)
                    {
                        for (int x = zeroCoord + 1 - (imgSize.Width / 2) + _X, m = 0; m < imgSize.Width; x++, m++)
                        {
                            if (cursor[n * imgSize.Height + m] != 0 &&
                                y >= 0 && y < 256 && x >= 0 && x < 256)
                                pixels[y * 256 + x] = cursor[n * imgSize.Height + m];
                        }
                    }
                    return Do.PixelsToImage(pixels, 256, 256);
                }
                else
                {
                    if (scarecrowButton.Checked)
                        pixels = (int[])characterImages[11].Clone();
                    else
                        pixels = (int[])characterImages[index + 6].Clone();

                    palette = Model.NumeralPaletteSet.Palette;
                    cursor = Do.GetPixelRegion(Model.NumeralGraphics, 0x20, palette, 16, 12, 0, 2, 2, 0);

                    imgSize = new Size(16, 16);
                    zeroCoord = 128;
                    if (scarecrowButton.Checked)
                    {
                        _X = CursorX_Scarecrow * 8; _Y = CursorY_Scarecrow * 8;
                    }
                    else
                    {
                        _X = CursorX * 8; _Y = CursorY * 8;
                    }

                }

                for (int y = zeroCoord - imgSize.Height - _Y, n = 0; n < imgSize.Height; y++, n++)
                {
                    for (int x = zeroCoord - imgSize.Width - _X, m = 0; m < imgSize.Width; x++, m++)
                    {
                        if (cursor[n * imgSize.Height + m] != 0 &&
                            y >= 0 && y < 256 && x >= 0 && x < 256)
                            pixels[y * 256 + x] = cursor[n * imgSize.Height + m];
                    }
                }
                return Do.PixelsToImage(pixels, 256, 256);
            }
        }

        // parent editor
        private AlliesEditor parentEditor;
        #endregion
        // constructor
        public Coordinates(AlliesEditor parentEditor)
        {
            this.parentEditor = parentEditor;
            InitializeComponent();
            InitializeCharacters();
            index = 0;
            currentSprite = 0;
            RefreshCharacter();
            //
            this.History = new History(this, false);
        }
        // functions
        private void InitializeCharacters()
        {
            this.Updating = true;

            characterImages = new int[12][];
            int[] baseSprite = new int[] { 2, 8, 14, 26, 20, 39 };

            for (int i = 0; i < 6; i++)
            {
                //int baseSprite = Model.ROM[0x0318A3 + i];
                Sprite sprite = Model.Sprites[baseSprite[i]];
                Animation animation = Model.Animations[sprite.AnimationPacket];
                Sequence sequence = null;
                int sequenceIndex = Model.ROM[0x031881];
                if (sequenceIndex < animation.Sequences.Count)
                    sequence = animation.Sequences[sequenceIndex];
                int moldIndex = 0;
                if (sequence != null && sequence.Frames.Count >= 0)
                    moldIndex = sequence.Frames[0].Mold;
                characterImages[i] = sprite.GetPixels(true, false, moldIndex, 0, false, false);
            }
            for (int i = 6; i < 12; i++)
            {
                characterImages[i] = Model.Sprites[baseSprite[i - 6]].GetPixels();
            }

            this.Updating = false;
        }
        public void RefreshCharacter()
        {
            if (this.Updating)
                return;
            this.Updating = true;

            this.characterTargetArrowX.Enabled = true;
            this.characterTargetArrowX.Maximum = 255;
            this.characterTargetArrowY.Maximum = 255;

            int X = 0, Y = 0;
            if (scarecrowButton.Checked)
            {
                if (currentSprite == 0) { X = 255; Y = SpriteABXY_Y_Scarecrow; }
                else { X = CursorX_Scarecrow; Y = CursorY_Scarecrow; }
            }
            else
            {
                if (currentSprite == 0) { X = 255; Y = SpriteABXY_Y; }
                else { X = CursorX; Y = CursorY; }
            }

            this.characterTargetArrowX.Value = X;
            this.characterTargetArrowY.Value = Y;

            if (currentSprite == 0)
            {
                this.characterTargetArrowX.Enabled = false;
                this.characterTargetArrowX.Maximum = 255;
                this.characterTargetArrowY.Maximum = 255;
            }
            else 
            {
                this.characterTargetArrowX.Maximum = 15;
                this.characterTargetArrowY.Maximum = 15;
            }

            //
            CharacterPaintBoxImage = new Bitmap(CharacterImage);
            pictureBoxCharacter.Invalidate();
            //
            this.Updating = false;
        }
        #region Event Handlers
        private void characterName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
        }
        private void pictureBoxCharacter_MouseDown(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxCharacter_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxCharacter.Invalidate();
        }
        private void pictureBoxCharacter_Paint(object sender, PaintEventArgs e)
        {
            if (CharacterPaintBoxImage != null)
                e.Graphics.DrawImage(CharacterPaintBoxImage, 0, 0);
        }
        private void pictureBoxCharacter_MouseMove_Cursor(object sender, MouseEventArgs e)
        {
            int x = 15 - (e.X / 8); int y = 15 - (e.Y / 8);
            if (x > 15) x = 15; if (x < 0) x = 0;
            if (y > 15) y = 15; if (y < 0) y = 0;
            if (e.Button == MouseButtons.Left)
            {
                if (overTarget)
                {
                    if (characterTargetArrowX.Value != x && characterTargetArrowY.Value != y)
                        waitBothCoords = true;
                    characterTargetArrowX.Value = x;
                    waitBothCoords = false;
                    characterTargetArrowY.Value = y;
                }
            }
            else
            {
                if ((128 - (characterTargetArrowX.Value * 8) > e.X && 128 - (characterTargetArrowX.Value * 8) < e.X + 16) &&
                    (128 - (characterTargetArrowY.Value * 8) > e.Y && 128 - (characterTargetArrowY.Value * 8) < e.Y + 16))
                {
                    pictureBoxCharacter.Cursor = Cursors.Hand;
                    overTarget = true;
                }
                else
                {
                    pictureBoxCharacter.Cursor = Cursors.Arrow;
                    overTarget = false;
                }
            }
        }
        private void pictureBoxCharacter_MouseMove_ABXY(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y + (ABXY_Height / 2 + ABXY_Height);
            if (x > 255) x = 255; if (x < 0) x = 0;
            if (y > 255) y = 255; if (y < 0) y = 0;
            if (e.Button == MouseButtons.Left)
            {
                if (overTarget)
                {
                    if (characterTargetArrowX.Value != x && characterTargetArrowY.Value != y)
                        waitBothCoords = true;
                    characterTargetArrowX.Value = 255;
                    waitBothCoords = false;
                    characterTargetArrowY.Value = y;
                }
            }
            else // zeroCoord + (imgSize.Height / 2) + _Y
            {
                if ((128 + (ABXY_Width / 2) > e.X && 128 + (ABXY_Width / 2) < e.X + 48) &&
                    (-128 + (ABXY_Height / 2) + ABXY_Height + (characterTargetArrowY.Value) > e.Y && -128 + (ABXY_Height / 2) + ABXY_Height + (characterTargetArrowY.Value) < e.Y + 48))
                {
                    pictureBoxCharacter.Cursor = Cursors.Hand;
                    overTarget = true;
                }
                else
                {
                    pictureBoxCharacter.Cursor = Cursors.Arrow;
                    overTarget = false;
                }
            }
        }
        private void characterTargetArrowX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Modified = true;
            //
            if (scarecrowButton.Checked)
            {
                if (currentSprite == 0) { }
                else CursorX_Scarecrow = (byte)characterTargetArrowX.Value;
            }
            else
            {
                if (currentSprite == 0) { }
                else CursorX = (byte)characterTargetArrowX.Value;
            }
            //
            if (waitBothCoords)
                return;
            CharacterPaintBoxImage = new Bitmap(CharacterImage);
            pictureBoxCharacter.Invalidate();
        }
        private void characterTargetArrowY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Modified = true;
            //
            if (scarecrowButton.Checked)
            {
                if (currentSprite == 0) SpriteABXY_Y_Scarecrow = (byte)characterTargetArrowY.Value;
                else CursorY_Scarecrow = (byte)characterTargetArrowY.Value;
            }
            else
            {
                if (currentSprite == 0) SpriteABXY_Y = (byte)characterTargetArrowY.Value;
                else CursorY = (byte)characterTargetArrowY.Value;
            }
            //
            if (waitBothCoords)
                return;
            CharacterPaintBoxImage = new Bitmap(CharacterImage);
            pictureBoxCharacter.Invalidate();
        }

        private void scarecrowButton_Click(object sender, EventArgs e)
        {
            RefreshCharacter();
            if (waitBothCoords)
                return;
            CharacterPaintBoxImage = new Bitmap(CharacterImage);
            pictureBoxCharacter.Invalidate();
        }

        private void cursorSpriteChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBoxCharacter.MouseMove -= pictureBoxCharacter_MouseMove_Cursor;
            this.pictureBoxCharacter.MouseMove -= pictureBoxCharacter_MouseMove_ABXY;
            if (currentSprite == 0)
            {
                this.pictureBoxCharacter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCharacter_MouseMove_ABXY);

            }
            else
            {
                this.pictureBoxCharacter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCharacter_MouseMove_Cursor);
            }

            RefreshCharacter();
            if (waitBothCoords)
                return;
            CharacterPaintBoxImage = new Bitmap(CharacterImage);
            pictureBoxCharacter.Invalidate();
        }

        #endregion
    }
}
