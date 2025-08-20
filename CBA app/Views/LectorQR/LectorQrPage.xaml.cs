using CBA_app.Services;
using CommunityToolkit.Maui.Views;
using ZXing.Net.Maui;

namespace CBA_app.Views.LectorQR;

public partial class LectorQrPage : Popup
{
    Entry textbox;
    private bool isPopupClosed = false;

    public LectorQrPage(ref Entry entry)
    {
        try
        {
            InitializeComponent();
            barcodeReaderView.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                AutoRotate = true,
                Multiple = false,
                Formats = ZXing.Net.Maui.BarcodeFormat.Codabar |
                          ZXing.Net.Maui.BarcodeFormat.Code128 |
                          ZXing.Net.Maui.BarcodeFormat.QrCode |
                          ZXing.Net.Maui.BarcodeFormat.Code39 |
                          ZXing.Net.Maui.BarcodeFormat.Ean8 |
                          ZXing.Net.Maui.BarcodeFormat.Ean13 

                          //ZXing.Net.Maui.BarcodeFormat.Pdf417 |
                          //ZXing.Net.Maui.BarcodeFormat.Aztec |
                          //ZXing.Net.Maui.BarcodeFormat.DataMatrix |
                          //ZXing.Net.Maui.BarcodeFormat.Itf |
                          //ZXing.Net.Maui.BarcodeFormat.MaxiCode |
                          //ZXing.Net.Maui.BarcodeFormat.Msi |
                          //ZXing.Net.Maui.BarcodeFormat.Plessey |
                          //ZXing.Net.Maui.BarcodeFormat.Rss14 |
                          //ZXing.Net.Maui.BarcodeFormat.UpcE |
                          //ZXing.Net.Maui.BarcodeFormat.UpcA |
                          //ZXing.Net.Maui.BarcodeFormat.UpcEanExtension
            };
            textbox = entry;
        }
        catch (Exception ex)
        {
            DisplayMensajes.DisplayErrorAlert($"Error al inicializar la página: {ex.Message}");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            await DisplayMensajes.DisplayErrorAlert($"Error al cerrar el Popup: {ex.Message}");
        }
    }

    private void barcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        try
        {
            if (e.Results != null && e.Results.Any())
            {
                var barcode = e.Results.FirstOrDefault();

                if (barcode != null && !string.IsNullOrEmpty(barcode.Value))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            if (!isPopupClosed)
                            {
                                if (textbox != null)
                                {
                                    textbox.Text = barcode.Value;
                                    ClosePopup();
                                }
                                else
                                {
                                    DisplayMensajes.DisplayErrorAlert("El campo de texto no está disponible.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayMensajes.DisplayErrorAlert($"Error al actualizar el texto: {ex.Message}");
                        }
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        DisplayMensajes.DisplayErrorAlert("No se pudo leer un código de barras válido.");
                    });
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayMensajes.DisplayErrorAlert("No se detectaron códigos de barras.");
                });
            }
        }
        catch (Exception ex)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DisplayMensajes.DisplayErrorAlert($"Ocurrió un error inesperado: {ex.Message}");
            });
        }
    }

    private void ClosePopup()
    {
        try
        {
            if (!isPopupClosed)
            {
                isPopupClosed = true;
                barcodeReaderView.BarcodesDetected -= barcodeReaderView_BarcodesDetected;
                Close();
            }
        }
        catch (Exception ex)
        {
            DisplayMensajes.DisplayErrorAlert($"Error al cerrar el Popup: {ex.Message}");
        }
    }
}
