using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using ExtendedSystemObjects;

namespace Renderer
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Generate a playing field
    /// </summary>
    public partial class Aurora
    {
        public static readonly DependencyProperty Height = DependencyProperty.Register(nameof(Height),
            typeof(int),
            typeof(Aurora), null);

        public static readonly DependencyProperty Width = DependencyProperty.Register(nameof(Width),
            typeof(int),
            typeof(Aurora), null);

        public static readonly DependencyProperty Texture = DependencyProperty.Register(nameof(Texture),
            typeof(int),
            typeof(Aurora), null);

        public static readonly DependencyProperty Map = DependencyProperty.Register(nameof(Map),
            typeof(List<int>),
            typeof(Aurora), null);

        public static readonly DependencyProperty Grid = DependencyProperty.Register(nameof(Grid),
            typeof(bool),
            typeof(Aurora), null);

        public static readonly DependencyProperty Number = DependencyProperty.Register(nameof(Number),
            typeof(bool),
            typeof(Aurora), null);

        private Bitmap _LayerOne;
        private Bitmap _LayerTwo;
        private Bitmap _LayerThree;

        public int DependencyHeight
        {
            get => (int)GetValue(Height);
            set => SetValue(Height, value);
        }


        public int DependencyWidth
        {
            get => (int)GetValue(Width);
            set => SetValue(Width, value);
        }

        public int DependencyTexture
        {
            get => (int)GetValue(Texture);
            set => SetValue(Texture, value);
        }

        public List<int> DependencyMap
        {
            get => (List<int>)GetValue(Map);
            set => SetValue(Map, value);
        }

        public bool DependencyGrid
        {
            get => (bool)GetValue(Grid);
            set => SetValue(Grid, value);
        }

        public bool DependencyNumber
        {
            get => (bool)GetValue(Number);
            set => SetValue(Number, value);
        }

        public Aurora()
        {
            InitializeComponent();
            Initiate();
        }

        private void Initiate()
        {
            if (DependencyWidth == 0 || DependencyHeight == 0 || DependencyTexture == 0) return;

            _LayerOne = new Bitmap(DependencyWidth * DependencyTexture, DependencyHeight * DependencyTexture);
            Generate();

            if (DependencyGrid) _LayerTwo = new Bitmap(DependencyWidth * DependencyTexture, DependencyHeight * DependencyTexture);
            _LayerThree = new Bitmap(DependencyWidth * DependencyTexture, DependencyHeight * DependencyTexture);
        }

        private void Generate()
        {
            var layers = DependencyMap.ChunkBy(DependencyHeight);


            for (var y = 0; y < layers.Count; y++)
            {
                var slice = layers[y];

                for (var x = 0; x < slice.Count; x++)
                {
                    var id = slice[x];
                    if(id == 0) continue;

                    DrawMap(x, y, id);
                }
            }
        }

        private void DrawMap(int i, int i1, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
