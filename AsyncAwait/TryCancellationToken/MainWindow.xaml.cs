using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TryCancellationToken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, System.ComponentModel.INotifyPropertyChanged
    {
        private int counter = 1;
        private bool makeCancelVisible = false;
        public bool MakeCancelVisible {
            get { return makeCancelVisible;  }
            set { makeCancelVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MakeCancelVisible"));
            } }

        private CancellationTokenSource cts = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnStartParsing(object sender, RoutedEventArgs e)
        {
            MakeCancelVisible = true;
            cts = new CancellationTokenSource();
            try {
                var progress = new Progress<int>();
                progress.ProgressChanged += (s, ee) => {
                    string threadID = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                    ProgressTextBox.Text = ee.ToString() + "---" + threadID; };
                // Putting a ConfigureAwait(false) will mean that we continue on the same context that the await ran on. 
                // That is, we dont run on the captured context. Hence the ui control setting later in the code will cause
                // and exception since you cant change UI controls on a non-UI thread.
                await ParseFiles(cts.Token, progress);                
            }
            catch (OperationCanceledException o) {
                MessageBox.Show(o.Message + System.Environment.UserName);
            }
            finally {
                ExplorerContentTextBlock.Text = " bla " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                cts.Dispose();
            }
        }
        private void OnCancelOperation(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();            
        }

        private Task ParseFiles(CancellationToken cancellationToken, IProgress<int> progress)
        {
            return Task.Run(async () => {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, cancellationToken);
                    progress.Report(counter++);
                    string threadID = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                    ExplorerContentTextBlock.Dispatcher.Invoke(() =>
                    {
                        ExplorerContentTextBlock.Text = DateTime.Now.ToLocalTime().ToString() + "---" + threadID;
                        //ExplorerContentTextBlock.Text = threadID + " " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                    });
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
            , cancellationToken);
        }

    }
}
