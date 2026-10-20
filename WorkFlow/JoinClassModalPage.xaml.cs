using System.Text.RegularExpressions;

namespace WorkFlow;

public partial class JoinClassModalPage : ContentPage
{
    public JoinClassModalPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Valida el código de clase mientras el usuario escribe y activa el botón "Unirse"
    /// </summary>
    private void OnCodeTextChanged(object sender, TextChangedEventArgs e)
    {
        string input = e.NewTextValue?.Trim() ?? string.Empty;

        // Validar longitud habitual (5 a 7 caracteres alfanuméricos)
        bool isValidFormat = Regex.IsMatch(input, "^[a-zA-Z0-9]{5,7}$");

        if (input.Length >= 5 && !isValidFormat)
        {
            ErrorMessageLabel.IsVisible = true;
            CodeInputContainer.Stroke = Color.FromArgb("#EF4444"); // Borde rojo de error
            JoinButton.IsEnabled = false;
            JoinButtonBorder.Opacity = 0.5;
        }
        else
        {
            ErrorMessageLabel.IsVisible = false;
            CodeInputContainer.Stroke = Color.FromArgb("#5EEAD4"); // Borde cian de WorkFlow

            if (isValidFormat)
            {
                JoinButton.IsEnabled = true;
                JoinButtonBorder.Opacity = 1.0;
            }
            else
            {
                JoinButton.IsEnabled = false;
                JoinButtonBorder.Opacity = 0.5;
            }
        }
    }

    /// <summary>
    /// Resalta el contenedor de texto al recibir el foco
    /// </summary>
    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        CodeInputContainer.Stroke = Color.FromArgb("#5EEAD4");
    }

    /// <summary>
    /// Restaura el borde cuando pierde el foco si no hay errores
    /// </summary>
    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (!ErrorMessageLabel.IsVisible)
        {
            // Vuelve al color neutro según el tema
            CodeInputContainer.Stroke = Application.Current?.RequestedTheme == AppTheme.Dark
                ? Color.FromArgb("#334155")
                : Color.FromArgb("#CBD5E1");
        }
    }

    /// <summary>
    /// Cierra la ventana modal sin aplicar cambios
    /// </summary>
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    /// <summary>
    /// Acción de cambiar cuenta
    /// </summary>
    private async void OnChangeAccountClicked(object sender, EventArgs e)
    {
        await DisplayAlert("WorkFlow", "Redirigiendo a selección de cuenta institucional...", "Aceptar");
    }

    /// <summary>
    /// Envía el código al backend para registrar la clase
    /// </summary>
    private async void OnJoinClassClicked(object sender, EventArgs e)
    {
        string code = ClassCodeEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(code))
            return;

        // Desactivar controles y mostrar animación de carga
        JoinButton.IsEnabled = false;
        ClassCodeEntry.IsEnabled = false;
        LoadingSpinner.IsVisible = true;
        LoadingSpinner.IsRunning = true;

        try
        {
            // Simulación de consumo de Web API en ASP.NET Core
            await Task.Delay(1200);

            // Ejemplo de respuesta exitosa
            await DisplayAlert("Éxito", $"Te has unido correctamente con el código: {code.ToUpper()}", "Aceptar");

            // Cerrar la ventana modal y volver a MainPage
            await Navigation.PopModalAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se encontró ninguna clase con ese código o ya estás inscrito.", "Reintentar");
        }
        finally
        {
            LoadingSpinner.IsRunning = false;
            LoadingSpinner.IsVisible = false;
            ClassCodeEntry.IsEnabled = true;
            JoinButton.IsEnabled = true;
        }
    }
}