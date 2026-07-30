using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace WorldMap2026
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainMenu = new MenuStrip();
            saveWorldMenuItem = new ToolStripMenuItem();
            saveWorldAsCopyMenuItem = new ToolStripMenuItem();
            openWorldMenuItem = new ToolStripMenuItem();
            createWorldMenuItem = new ToolStripMenuItem();
            objectsToolStrip = new ToolStrip();
            btnWindmill = new ToolStripButton();
            btnField = new ToolStripButton();
            btnTree = new ToolStripButton();
            btnFlower = new ToolStripButton();
            sep1 = new ToolStripSeparator();
            btnEraser = new ToolStripButton();
            btnPalette = new ToolStripButton();
            sep2 = new ToolStripSeparator();
            btnGrass = new ToolStripButton();
            btnWater = new ToolStripButton();
            btnSand = new ToolStripButton();
            btnRock = new ToolStripButton();
            btnClearAll = new ToolStripButton();
            mainStatusStrip = new StatusStrip();
            lblCoords = new ToolStripStatusLabel();
            lblCurrentTool = new ToolStripStatusLabel();
            lblStats = new ToolStripStatusLabel();
            mapContainerPanel = new Panel();
            mapPictureBox = new PictureBox();
            mainMenu.SuspendLayout();
            objectsToolStrip.SuspendLayout();
            mainStatusStrip.SuspendLayout();
            mapContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mapPictureBox).BeginInit();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.Items.AddRange(new ToolStripItem[] { saveWorldMenuItem, saveWorldAsCopyMenuItem, openWorldMenuItem, createWorldMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(1008, 24);
            mainMenu.TabIndex = 0;
            // 
            // saveWorldMenuItem
            // 
            saveWorldMenuItem.Name = "saveWorldMenuItem";
            saveWorldMenuItem.Size = new Size(78, 20);
            saveWorldMenuItem.Text = "Сохранить";
            saveWorldMenuItem.Click += saveWorldMenuItem_Click;
            // 
            // saveWorldAsCopyMenuItem
            // 
            saveWorldAsCopyMenuItem.Name = "saveWorldAsCopyMenuItem";
            saveWorldAsCopyMenuItem.Size = new Size(139, 20);
            saveWorldAsCopyMenuItem.Text = "Сохранить как копию";
            saveWorldAsCopyMenuItem.Click += saveWorldAsCopyMenuItem_Click;
            // 
            // openWorldMenuItem
            // 
            openWorldMenuItem.Name = "openWorldMenuItem";
            openWorldMenuItem.Size = new Size(73, 20);
            openWorldMenuItem.Text = "Загрузить";
            openWorldMenuItem.Click += openWorldMenuItem_Click;
            // 
            // createWorldMenuItem
            // 
            createWorldMenuItem.Name = "createWorldMenuItem";
            createWorldMenuItem.Size = new Size(101, 20);
            createWorldMenuItem.Text = "Создать новый";
            createWorldMenuItem.Click += createWorldMenuItem_Click;
            // 
            // objectsToolStrip
            // 
            objectsToolStrip.Items.AddRange(new ToolStripItem[] { btnWindmill, btnField, btnTree, btnFlower, sep1, btnEraser, btnPalette, sep2, btnGrass, btnWater, btnSand, btnRock });
            objectsToolStrip.Location = new Point(0, 24);
            objectsToolStrip.Name = "objectsToolStrip";
            objectsToolStrip.Size = new Size(1008, 25);
            objectsToolStrip.TabIndex = 1;
            // 
            // btnWindmill
            // 
            btnWindmill.CheckOnClick = true;
            btnWindmill.Name = "btnWindmill";
            btnWindmill.Size = new Size(68, 22);
            btnWindmill.Text = "Мельница";
            btnWindmill.Click += ToolButton_Click;
            // 
            // btnField
            // 
            btnField.CheckOnClick = true;
            btnField.Name = "btnField";
            btnField.Size = new Size(40, 22);
            btnField.Text = "Поле";
            btnField.Click += ToolButton_Click;
            // 
            // btnTree
            // 
            btnTree.CheckOnClick = true;
            btnTree.Name = "btnTree";
            btnTree.Size = new Size(51, 22);
            btnTree.Text = "Дерево";
            btnTree.Click += ToolButton_Click;
            // 
            // btnFlower
            // 
            btnFlower.CheckOnClick = true;
            btnFlower.Name = "btnFlower";
            btnFlower.Size = new Size(46, 22);
            btnFlower.Text = "Цветы";
            btnFlower.Click += ToolButton_Click;
            // 
            // sep1
            // 
            sep1.Name = "sep1";
            sep1.Size = new Size(6, 25);
            // 
            // btnEraser
            // 
            btnEraser.CheckOnClick = true;
            btnEraser.Name = "btnEraser";
            btnEraser.Size = new Size(55, 22);
            btnEraser.Text = "Удалить";
            btnEraser.Click += ToolButton_Click;
            // 
            // btnPalette
            // 
            btnPalette.Name = "btnPalette";
            btnPalette.Size = new Size(58, 22);
            btnPalette.Text = "Палитра";
            btnPalette.Click += ToolButton_Click;
            // 
            // sep2
            // 
            sep2.Name = "sep2";
            sep2.Size = new Size(6, 25);
            // 
            // btnGrass
            // 
            btnGrass.CheckOnClick = true;
            btnGrass.Name = "btnGrass";
            btnGrass.Size = new Size(42, 22);
            btnGrass.Text = "Трава";
            btnGrass.Click += ToolButton_Click;
            // 
            // btnWater
            // 
            btnWater.CheckOnClick = true;
            btnWater.Name = "btnWater";
            btnWater.Size = new Size(37, 22);
            btnWater.Text = "Вода";
            btnWater.Click += ToolButton_Click;
            // 
            // btnSand
            // 
            btnSand.CheckOnClick = true;
            btnSand.Name = "btnSand";
            btnSand.Size = new Size(45, 22);
            btnSand.Text = "Песок";
            btnSand.Click += ToolButton_Click;
            // 
            // btnRock
            // 
            btnRock.CheckOnClick = true;
            btnRock.Name = "btnRock";
            btnRock.Size = new Size(47, 22);
            btnRock.Text = "Камни";
            btnRock.Click += ToolButton_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(23, 23);
            // 
            // mainStatusStrip
            // 
            mainStatusStrip.Items.AddRange(new ToolStripItem[] { lblCoords, lblCurrentTool, lblStats });
            mainStatusStrip.Location = new Point(0, 707);
            mainStatusStrip.Name = "mainStatusStrip";
            mainStatusStrip.Size = new Size(1008, 22);
            mainStatusStrip.TabIndex = 2;
            // 
            // lblCoords
            // 
            lblCoords.Name = "lblCoords";
            lblCoords.Size = new Size(66, 17);
            lblCoords.Text = "[ X: - | Y: - ]";
            // 
            // lblCurrentTool
            // 
            lblCurrentTool.Name = "lblCurrentTool";
            lblCurrentTool.Size = new Size(809, 17);
            lblCurrentTool.Spring = true;
            lblCurrentTool.Text = "[ Инструмент: Не выбран ]";
            // 
            // lblStats
            // 
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(118, 17);
            lblStats.Text = "[ Всего объектов: 0 ]";
            // 
            // mapContainerPanel
            // 
            mapContainerPanel.AutoScroll = true;
            mapContainerPanel.Controls.Add(mapPictureBox);
            mapContainerPanel.Dock = DockStyle.Fill;
            mapContainerPanel.Location = new Point(0, 49);
            mapContainerPanel.Name = "mapContainerPanel";
            mapContainerPanel.Size = new Size(1008, 658);
            mapContainerPanel.TabIndex = 3;
            // 
            // mapPictureBox
            // 
            mapPictureBox.Location = new Point(0, 0);
            mapPictureBox.MinimumSize = new Size(1024, 756);
            mapPictureBox.Name = "mapPictureBox";
            mapPictureBox.Size = new Size(1024, 756);
            mapPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            mapPictureBox.TabIndex = 0;
            mapPictureBox.TabStop = false;
            mapPictureBox.Paint += mapPictureBox_Paint;
            mapPictureBox.MouseClick += mapPictureBox_Click;
            mapPictureBox.MouseMove += mapPictureBox_MouseMove;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 729);
            Controls.Add(mapContainerPanel);
            Controls.Add(mainStatusStrip);
            Controls.Add(objectsToolStrip);
            Controls.Add(mainMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenu;
            MinimumSize = new Size(1024, 768);
            Name = "MainForm";
            Text = "Карта";
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            objectsToolStrip.ResumeLayout(false);
            objectsToolStrip.PerformLayout();
            mainStatusStrip.ResumeLayout(false);
            mainStatusStrip.PerformLayout();
            mapContainerPanel.ResumeLayout(false);
            mapContainerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mapPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem saveWorldMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorldAsCopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorldMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createWorldMenuItem;
        private System.Windows.Forms.ToolStrip objectsToolStrip;

        private System.Windows.Forms.ToolStripButton btnWindmill;
        private System.Windows.Forms.ToolStripButton btnField;
        private System.Windows.Forms.ToolStripButton btnTree;
        private System.Windows.Forms.ToolStripButton btnFlower;

        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton btnEraser;
        private System.Windows.Forms.ToolStripButton btnClearAll;
        private System.Windows.Forms.ToolStripSeparator sep2;

        private System.Windows.Forms.ToolStripButton btnGrass;
        private System.Windows.Forms.ToolStripButton btnWater;
        private System.Windows.Forms.ToolStripButton btnSand;
        private System.Windows.Forms.ToolStripButton btnRock;
        private System.Windows.Forms.ToolStripButton btnPalette;

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblCoords;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentTool;
        private System.Windows.Forms.ToolStripStatusLabel lblStats;

        private System.Windows.Forms.Panel mapContainerPanel;
        private System.Windows.Forms.PictureBox mapPictureBox;
    }
}