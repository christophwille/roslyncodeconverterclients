using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using RoslynCodeConverterClientLibrary.Proxies;
using RoslynCodeConverterClientLibrary.Proxies.Models;

namespace DesktopConverterClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            inputCode.Text = "public class Test {}";
            vbDefinition = HighlightingManager.Instance.GetDefinition("VB");
        }

        private IHighlightingDefinition vbDefinition;

        private async void runConversion_Click(object sender, RoutedEventArgs e)
        {
            outputCode.Text = "";
            outputCode.SyntaxHighlighting = vbDefinition;

            string code = inputCode.Text;
            converterCallInflight.Visibility = Visibility.Visible;
            runConversion.IsEnabled = false;

            try
            {
                var client = new RoslynCodeConverter();

                ConvertResponse result = await client.Converter.PostAsync(new ConvertRequest()
                {
                    Code = code,
                    RequestedConversion = "cs2vbnet"
                });

                if (true == result.ConversionOk)
                {
                    outputCode.Text = result.ConvertedCode;
                }
                else
                {
                    outputCode.SyntaxHighlighting = null;
                    outputCode.Text = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                outputCode.SyntaxHighlighting = null;
                outputCode.Text = ex.Message;
            }

            converterCallInflight.Visibility = Visibility.Collapsed;
            runConversion.IsEnabled = true;
        }

        private void loadInputFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "C# Files (*.cs) | *.cs";

            if (dlg.ShowDialog() ?? false)
            {
                inputCode.Load(dlg.FileName);
            }
        }
    }
}
