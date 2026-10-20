using System.Collections.ObjectModel;
using WorkFlow.Models;
namespace WorkFlow;

public partial class TaskDetailPage : ContentPage
{
    public AssignmentItem Assignment { get; set; }
    public string InstructionsText { get; set; } = string.Empty;
    public string SubmissionStatusLabel { get; set; } = "Sin entregar";
    public bool IsSubmitted { get; set; } = false;

    public ObservableCollection<StudentFileModel> StudentAttachments { get; set; }
    public ObservableCollection<PrivateCommentModel> PrivateComments { get; set; }

    public TaskDetailPage() : this(new AssignmentItem
    {
        Title = "1. Actividad de apertura: Marco Jurídico",
        DueDateText = "Vence: Mañana 23:59",
        StatusText = "Asignada",
        StatusBadgeBackground = Color.FromArgb("#162D34"),
        StatusTextColor = Color.FromArgb("#5EEAD4")
    })
    {
    }

    public TaskDetailPage(AssignmentItem selectedAssignment)
    {
        InitializeComponent();
        Assignment = selectedAssignment;

        InstructionsText = "Lee atentamente los artículos 1 al 15 del Código de Comercio proporcionados en el documento de apoyo. Realiza un resumen analítico destacando la definición de comerciante, los actos de comercio y las obligaciones comunes a todos los que ejercen el comercio. Adjunta tu documento en formato PDF o Word.";

        StudentAttachments = new ObservableCollection<StudentFileModel>();
        PrivateComments = new ObservableCollection<PrivateCommentModel>();

        BindingContext = this;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnMoreOptionsClicked(object sender, EventArgs e)
    {
        await DisplayActionSheet("Opciones de tarea", "Cancelar", null, "Reportar problema", "Copiar enlace de tarea");
    }

    private async void OnOpenTeacherAttachmentClicked(object sender, EventArgs e)
    {
        await DisplayAlert("WorkFlow Reader", "Abriendo vista previa del PDF 'Guia_Actividad_1_DerechoMercantil.pdf'...", "Aceptar");
    }

    private async void OnAddAttachmentClicked(object sender, EventArgs e)
    {
        if (IsSubmitted)
        {
            await DisplayAlert("WorkFlow", "Anula la entrega si deseas agregar más archivos.", "Aceptar");
            return;
        }

        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Selecciona tu archivo de tarea",
                FileTypes = FilePickerFileType.Pdf
            });

            if (result != null)
            {
                StudentAttachments.Add(new StudentFileModel
                {
                    FileName = result.FileName,
                    FileSizeText = "Documento PDF",
                    FileIcon = "📄",
                    FilePath = result.FullPath
                });
            }
        }
        catch (Exception)
        {
            StudentAttachments.Add(new StudentFileModel
            {
                FileName = "Tarea1_MarcoJuridico_Alvarez.pdf",
                FileSizeText = "PDF • 850 KB",
                FileIcon = "📄",
                FilePath = ""
            });
        }
    }

    private void OnRemoveAttachmentClicked(object sender, EventArgs e)
    {
        if (IsSubmitted)
            return;

        if (sender is ImageButton button && button.BindingContext is StudentFileModel file)
        {
            StudentAttachments.Remove(file);
        }
    }

    private async void OnSubmitWorkClicked(object sender, EventArgs e)
    {
        if (!IsSubmitted)
        {
            if (StudentAttachments.Count == 0)
            {
                bool confirmEmpty = await DisplayAlert(
                    "¿Entregar sin archivos?",
                    "No has adjuntado ningún archivo a esta tarea. ¿Deseas entregarla de todos modos?",
                    "Sí, entregar",
                    "Cancelar");

                if (!confirmEmpty)
                    return;
            }

            WorkSpinner.IsVisible = true;
            WorkSpinner.IsRunning = true;
            SubmitButton.IsEnabled = false;

            await Task.Delay(1000);

            IsSubmitted = true;
            SubmissionStatusLabel = "Entregada";
            SubmitButtonTextLabel.Text = "Anular entrega";
            SubmitButtonTextLabel.TextColor = Color.FromArgb("#EF4444");
            SubmitButtonBorder.Background = new SolidColorBrush(Color.FromArgb("#382020"));

            WorkSpinner.IsRunning = false;
            WorkSpinner.IsVisible = false;
            SubmitButton.IsEnabled = true;

            await DisplayAlert("¡Tarea Entregada!", "Tu trabajo ha sido enviado correctamente al docente.", "Aceptar");
        }
        else
        {
            bool confirmCancel = await DisplayAlert(
                "Anular entrega",
                "¿Estás seguro de que deseas anular la entrega para hacer cambios?",
                "Sí, anular",
                "Cancelar");

            if (confirmCancel)
            {
                IsSubmitted = false;
                SubmissionStatusLabel = "Sin entregar";
                SubmitButtonTextLabel.Text = "Entregar tarea";
                SubmitButtonTextLabel.TextColor = Color.FromArgb("#0F172A");
                SubmitButtonBorder.Background = (Brush)Resources["WorkflowBrandBrush"];

                await DisplayAlert("WorkFlow", "Se ha anulado la entrega. Puedes modificar tus archivos.", "Aceptar");
            }
        }
    }

    private void OnSendCommentClicked(object sender, EventArgs e)
    {
        string comment = PrivateCommentEntry.Text?.Trim() ?? string.Empty;

        if (!string.IsNullOrEmpty(comment))
        {
            PrivateComments.Add(new PrivateCommentModel
            {
                AuthorName = "Tú",
                Message = comment,
                TimeAgo = "Ahora"
            });

            PrivateCommentEntry.Text = string.Empty;
        }
    }
}
