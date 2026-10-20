namespace WorkFlow;

public partial class CreateClassModalPage : ContentPage
{
    // Imagen de portada por defecto
    private string selectedImageUri = "https://images.unsplash.com/photo-1517694712202-14dd9538aa97?auto=format&fit=crop&w=600&q=80";

    public CreateClassModalPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Actualiza la vista previa y valida que el campo obligatorio esté presente
    /// </summary>
    private void OnFormFieldsChanged(object sender, TextChangedEventArgs e)
    {
        string className = ClassNameEntry.Text?.Trim() ?? string.Empty;
        string group = GroupEntry.Text?.Trim() ?? string.Empty;
        string subject = SubjectEntry.Text?.Trim() ?? string.Empty;
        string room = RoomEntry.Text?.Trim() ?? string.Empty;

        // Actualizar textos en la tarjeta de vista previa
        PreviewTitleLabel.Text = string.IsNullOrWhiteSpace(className)
            ? "Nombre de la clase..."
            : className;

        PreviewGroupLabel.Text = string.IsNullOrWhiteSpace(group)
            ? "5IM6"
            : group;

        string detailSummary = string.Empty;
        if (!string.IsNullOrWhiteSpace(room) && !string.IsNullOrWhiteSpace(subject))
            detailSummary = $"{room}\n{subject}";
        else if (!string.IsNullOrWhiteSpace(room))
            detailSummary = room;
        else if (!string.IsNullOrWhiteSpace(subject))
            detailSummary = subject;
        else
            detailSummary = "Aula / Especialidad";

        PreviewDetailsLabel.Text = detailSummary;

        // Regla de validación: Requiere al menos el nombre de la clase
        bool isValid = !string.IsNullOrWhiteSpace(className);

        CreateButton.IsEnabled = isValid;
        CreateButtonBorder.Opacity = isValid ? 1.0 : 0.5;
    }

    /// <summary>
    /// Permite al docente seleccionar una imagen desde su dispositivo
    /// </summary>
    private async void OnPickCoverImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Selecciona una portada para la clase",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                selectedImageUri = result.FullPath;
                PreviewCoverImage.Source = ImageSource.FromFile(result.FullPath);
            }
        }
        catch (Exception)
        {
            await DisplayAlert("WorkFlow", "No fue posible cargar la imagen seleccionada.", "Aceptar");
        }
    }

    /// <summary>
    /// Cierra el modal sin guardar cambios
    /// </summary>
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    /// <summary>
    /// Envía la clase al backend de ASP.NET Core
    /// </summary>
    private async void OnCreateClassClicked(object sender, EventArgs e)
    {
        string className = ClassNameEntry.Text?.Trim() ?? string.Empty;
        string group = GroupEntry.Text?.Trim() ?? string.Empty;
        string subject = SubjectEntry.Text?.Trim() ?? string.Empty;
        string room = RoomEntry.Text?.Trim() ?? string.Empty;

        // Bloquear controles y mostrar spinner
        CreateButton.IsEnabled = false;
        CreationSpinner.IsVisible = true;
        CreationSpinner.IsRunning = true;

        try
        {
            // Simulación de llamada a la Web API (POST /api/courses)
            await Task.Delay(1200);

            // Generar código de acceso automático para la clase (ej. "7K2M9P")
            string generatedClassCode = "WF" + new Random().Next(1000, 9999);

            await DisplayAlert(
                "¡Clase Creada!",
                $"La materia '{className}' ha sido registrada con éxito.\n\nCódigo para alumnos: {generatedClassCode}",
                "Aceptar");

            // Cerrar modal
            await Navigation.PopModalAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se pudo registrar la clase. Intenta nuevamente.", "Reintentar");
        }
        finally
        {
            CreationSpinner.IsRunning = false;
            CreationSpinner.IsVisible = false;
            CreateButton.IsEnabled = true;
        }
    }
}