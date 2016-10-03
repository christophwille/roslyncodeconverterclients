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
using RoslynCodeConverter.Client;
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
            csharpDefinition = HighlightingManager.Instance.GetDefinition("C#");
        }

        private IHighlightingDefinition vbDefinition;
        private IHighlightingDefinition csharpDefinition;

        private async void runConversion_Click(object sender, RoutedEventArgs e)
        {
            outputCode.Text = "";
            string conversionType = SupportedConversions.CSharp2Vb;

            if (cs2vbnetRbtn.IsChecked == true)
            {
                outputCode.SyntaxHighlighting = vbDefinition;
            }
            else
            {
                outputCode.SyntaxHighlighting = csharpDefinition;
                conversionType = SupportedConversions.Vb2CSharp;
            }

            string code = inputCode.Text;
            converterCallInflight.Visibility = Visibility.Visible;
            runConversion.IsEnabled = false;

            try
            {
                var client = new RoslynCodeConverterClientLibrary.Proxies.RoslynCodeConverter();

                ConvertResponse result = await client.Converter.PostAsync(new ConvertRequest()
                {
                    Code = code,
                    RequestedConversion = conversionType
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
            OpenFileDialog dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "C# Files (*.cs) | *.cs|VB Files (*.vb) | *.vb"
            };

            if (dlg.ShowDialog() ?? false)
            {
                string filename = dlg.FileName;
                string extension = System.IO.Path.GetExtension(filename);

                if (0 == String.Compare(".cs", extension, StringComparison.InvariantCultureIgnoreCase))
                {
                    inputCode.SyntaxHighlighting = csharpDefinition;
                    cs2vbnetRbtn.IsChecked = true;
                }
                else
                {
                    // simply assume vb in the other case
                    inputCode.SyntaxHighlighting = vbDefinition;
                    vbnet2csRbtn.IsChecked = true;
                }

                inputCode.Load(filename);
            }
        }
    }
}
