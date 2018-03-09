using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows.Media;
using Common;
using Common.Messages;
using Common.Messaging;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Timer = System.Timers.Timer;

namespace GraphControl
{
    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Chart
    {
        private class GraphData
        {
            private class FixedSizedQueue<T> : Queue<T>
            {
                private int Size { get; set; }

                public FixedSizedQueue(int size)
                {
                    Size = size;
                }

                public new void Enqueue(T obj)
                {
                    base.Enqueue(obj);
                    lock (this)
                    {
                        while (Count > Size)
                        {
                            Dequeue();
                        }
                    }
                }
            }

            private readonly Global.Instructions.Colors _color;
            private readonly FixedSizedQueue<float> _x = new FixedSizedQueue<float>(100);
            private readonly FixedSizedQueue<float> _y = new FixedSizedQueue<float>(100);
            private readonly EnumerableDataSource<float> _source;
            private readonly EnumerableDataSource<float> _dataSource;

            /// <summary>
            /// Gets or sets the data source.
            /// </summary>
            /// <value>
            /// The data source.
            /// </value>
            public CompositeDataSource DataSource { get; private set; }

            /// <summary>
            /// Gets or sets the pen.
            /// </summary>
            /// <value>
            /// The pen.
            /// </value>
            public Pen Pen { get; private set; }

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>
            /// The description.
            /// </value>
            public PenDescription Description { get; private set; }

            /// <summary>
            /// Gets or sets the graph.
            /// </summary>
            /// <value>
            /// The graph.
            /// </value>
            public IPlotterElement Graph { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="GraphData"/> class.
            /// </summary>
            /// <param name="color">The color.</param>
            public GraphData(Global.Instructions.Colors color)
            {
                _color = color;
                _source = new EnumerableDataSource<float>(_x);
                _source.SetXMapping(x => x);
                _dataSource = new EnumerableDataSource<float>(_y);
                _dataSource.SetYMapping(y => y);

                DataSource = new CompositeDataSource(new IPointDataSource[] {_source, _dataSource});

                Pen = new Pen(GetBrush(), 2.0);

                Description = new PenDescription(_color.ToString());
            }

            private SolidColorBrush GetBrush()
            {
                switch (_color)
                {
                    case Global.Instructions.Colors.Red:
                        return Brushes.Red;
                    case Global.Instructions.Colors.Blue:
                        return Brushes.Blue;
                    case Global.Instructions.Colors.Green:
                        return Brushes.Green;
                    case Global.Instructions.Colors.Yellow:
                        return Brushes.Yellow;
                    case Global.Instructions.Colors.Black:
                        return Brushes.Black;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            internal void RaiseDataChanged()
            {
                _dataSource.RaiseDataChanged();
            }

            internal void AddX(float p)
            {
                _x.Enqueue(p);
            }

            internal void AddY(float p)
            {
                _y.Enqueue(p);
            }
        }

        private readonly Dictionary<int, GraphData> _graphs;
        private Timer _timer;
        private long _updateUi;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Chart" /> class.
        /// </summary>
        public Chart()
        {
            InitializeComponent();
            _graphs = new Dictionary<int, GraphData>();
        }

        /// <summary>
        /// Handles the data point.
        /// </summary>
        /// <param name="dataPoint">The data point.</param>
        public void HandleDataPoint(Execution.DataPoint dataPoint)
        {
            if (_timer == null)
            {
                _timer = new Timer(200);
                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
            }

            lock (_graphs)
            {
                GraphData data;
                if (_graphs.ContainsKey(dataPoint.SignalId))
                {
                    data = _graphs[dataPoint.SignalId];
                    data.AddX(dataPoint.TimeStamp);
                    data.AddY(dataPoint.Value);
                }
                else
                {
                    _graphs.Add(dataPoint.SignalId, new GraphData(dataPoint.Color));
                    data = _graphs[dataPoint.SignalId];
                    data.Graph = ChartControl.AddLineGraph(data.DataSource, data.Pen, data.Description);
                }

                if (Interlocked.Read(ref _updateUi) != 0) return;

                data.RaiseDataChanged();
                ChartControl.FitToView();
                Interlocked.Increment(ref _updateUi);
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Interlocked.Exchange(ref _updateUi, 0);
        }

        /// <summary>
        /// Clears the graph.
        /// </summary>
        public void ClearGraph()
        {
            if (_timer == null) return;

            foreach (var graph in _graphs)
            {
                graph.Value.Graph.Remove();
                ChartControl.FitToView();
            }

            _timer.Dispose();
            _timer = null;
            _graphs.Clear();
        }
    }
}