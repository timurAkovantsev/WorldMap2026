using System.Media;
using WorldMap2026.Presenter;
using WorldMap2026.View;

namespace WorldMap2026
{
    public partial class MainForm : Form, IMapView
    {
        private const int CELL_SIZE = 40;

        private bool _isWorldLoaded = false;
        private MapRenderer _renderer = new MapRenderer();
        private CellRenderInfo[][]? _renderGrid;
        private List<ObjectRenderInfo>? _renderObjects;

        public event Action<int, int>? CreateNewWorldRequested;
        public event Action<string>? SaveWorldRequested;
        public event Action? QuickSaveRequested;
        public event Action<string>? LoadWorldRequested;
        public event Action<Point>? CursorMoved;
        public event Action<ToolType, Point>? CellOnClick;

        private readonly SoundPlayer _successPlayer = new SoundPlayer(Properties.Resources.successSound);
        private readonly SoundPlayer _failPlayer = new SoundPlayer(Properties.Resources.failSound);

        private ToolType _currentSelectedTool = ToolType.None;
        public ToolType CurrentSelectedTool => _currentSelectedTool;


        public MainForm()
        {
            InitializeComponent();
            mapContainerPanel.AutoScroll = true;
            objectsToolStrip.ImageScalingSize = new Size(32, 32);

            LoadImagesFromResources();

        }

        private void saveWorldMenuItem_Click(object sender, EventArgs e)
        {
            QuickSaveRequested?.Invoke();
        }

        private void openWorldMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Файлы мира JSON (*.json)|*.json";
                ofd.Title = "Загрузить мир";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadWorldRequested?.Invoke(ofd.FileName);
                }
            }
        }

        private void createWorldMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите размеры карты (Ширина x Высота, например: 30x20):",
                "Новый мир",
                "20x20");

            if (string.IsNullOrWhiteSpace(input))
                return;

            input = input.ToLower().Replace('х', ' ').Replace('x', ' ');

            string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int width) && width >= 10 &&
                int.TryParse(parts[1], out int height) && height >= 10)
            {
                mapPictureBox.Size = new Size(width * CELL_SIZE, height * CELL_SIZE);

                CreateNewWorldRequested?.Invoke(width, height);
            }
            else
            {
                MessageBox.Show(
                    "Пожалуйста, введите два положительных числа больше 10.\nПример правильного ввода: 30x20",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ToolButton_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton clickedButton)
            {
                foreach (ToolStripItem item in objectsToolStrip.Items)
                {
                    if (item is ToolStripButton otherButton && otherButton != clickedButton)
                        otherButton.Checked = false;
                }

                clickedButton.Checked = true;
                lblCurrentTool.Text = $"[ Инструмент: {clickedButton.Text} ]";

                _currentSelectedTool = clickedButton.Name switch
                {
                    "btnEraser" => ToolType.Eraser,
                    "btnPalette" => ToolType.Palette,
                    "btnGrass" => ToolType.Grass,
                    "btnSand" => ToolType.Sand,
                    "btnWater" => ToolType.Water,
                    "btnRock" => ToolType.Rock,
                    "btnWindmill" => ToolType.Windmill,
                    "btnTree" => ToolType.Tree,
                    "btnField" => ToolType.Field,
                    "btnFlower" => ToolType.Flower,
                    _ => ToolType.None
                };

                if (clickedButton.Image != null)
                {
                    Bitmap bmp = new Bitmap(clickedButton.Image, new Size(32, 32));

                    IntPtr hIcon = bmp.GetHicon();

                    mapPictureBox.Cursor = new Cursor(hIcon);
                }
                else
                {
                    mapPictureBox.Cursor = Cursors.Default;
                }
            }
        }

        private void mapPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (_renderGrid == null || _renderObjects == null)
            {
                string message = "Мир не загружен. Создайте или откройте карту.";
                Font font = new Font("Arial", 14, FontStyle.Bold);
                Brush textBrush = new SolidBrush(Color.DarkGray);
                g.DrawString(message, font, textBrush, new PointF(50, 50));
                return;
            }
            
            _renderer.DrawWorld(g, _renderGrid, _renderObjects, CELL_SIZE);
        }

        public void UpdateMapData(CellRenderInfo[][] grid, List<ObjectRenderInfo> objects)
        {
            _renderGrid = grid;
            _renderObjects = objects;
            _isWorldLoaded = true;
            mapPictureBox.Invalidate();
        }

        public void UpdateStatisticsDisplay(string statsText)
        {
            lblStats.Text = statsText;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OnSuccess()
        {
            _successPlayer.Play();
        }


        private void LoadImagesFromResources()
        {
            try
            {
                btnWindmill.Image = Properties.Resources.windmill;
                btnField.Image = Properties.Resources.fieldTool;
                btnTree.Image = Properties.Resources.tree;
                btnFlower.Image = Properties.Resources.flowerTool;

                btnEraser.Image = Properties.Resources.eraser;
                btnPalette.Image = Properties.Resources.palette;

                btnGrass.Image = Properties.Resources.grass;
                btnWater.Image = Properties.Resources.water;
                btnSand.Image = Properties.Resources.sand;
                btnRock.Image = Properties.Resources.rock;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось загрузить некоторые ресурсы (картинки).\nТекст ошибки: {ex.Message}",
                    "Внимание: Ошибка ресурсов",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void mapPictureBox_Click(object sender, MouseEventArgs e)
        {
            if (!_isWorldLoaded) return;

            int gridX = e.X / CELL_SIZE;
            int gridY = e.Y / CELL_SIZE;

            if (_currentSelectedTool == ToolType.None) return;

            CellOnClick?.Invoke(_currentSelectedTool, new Point(gridX, gridY));
        }

        private void mapPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            CursorMoved?.Invoke(new Point(e.X / CELL_SIZE, e.Y / CELL_SIZE));
        }


        public void OnFail()
        {
            _failPlayer.Play();
        }

        public Color GetColorForChange()
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.AllowFullOpen = true;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                return colorDialog.Color;
            }

            return Color.Empty;
        }

        private void saveWorldAsCopyMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Файлы мира JSON (*.json)|*.json";
                sfd.Title = "Сохранить текущий мир";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveWorldRequested?.Invoke(sfd.FileName);
                }
            }
        }
        public void UpdateCursorPosition(string positionText)
        {
            lblCoords.Text = positionText;
        }
    }
}