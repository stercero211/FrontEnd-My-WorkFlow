using System.Collections.ObjectModel;
using System.Collections;
using WorkFlow.Models;

namespace WorkFlow;

public partial class MainPage : ContentPage
{
    public ObservableCollection<CourseItem> EnrolledCourses { get; set; }
    public ObservableCollection<FeedPostItem> RecentPosts { get; set; }

    public MainPage()
    {
        InitializeComponent();
        LoadMockData();
        BindingContext = this;
    }

    private void LoadMockData()
    {
        EnrolledCourses = new ObservableCollection<CourseItem>
        {
            new CourseItem
            {
                Group = "5IM6",
                Title = "DERECHO MERCANTIL",
                Teacher = "Prof. Alejandro Cruz",
                ClassCode = "WF5M6M",
                ImageUrl = "https://images.unsplash.com/photo-1589829545856-d10d557cf95f?auto=format&fit=crop&w=600&q=80"
            },
            new CourseItem
            {
                Group = "5IM6",
                Title = "MODELADO DE SISTEMAS",
                Teacher = "Prof. Godelinda Melgoza Ponce",
                ClassCode = "WF5M6S",
                ImageUrl = "https://images.unsplash.com/photo-1517694712202-14dd9538aa97?auto=format&fit=crop&w=600&q=80"
            },
            new CourseItem
            {
                Group = "5IM6",
                Title = "DESARROLLO MÓVIL .NET MAUI",
                Teacher = "Academia de Informática CECyT 13",
                ClassCode = "WF5M6P",
                ImageUrl = "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?auto=format&fit=crop&w=600&q=80"
            }
        };

        RecentPosts = new ObservableCollection<FeedPostItem>
        {
            new FeedPostItem
            {
                Title = "Nueva tarea: Cuadro Sinóptico (Actos de comercio)",
                Subtitle = "5IM6 Derecho Mercantil • Publicado hoy",
                Icon = "📋",
                HasDueDate = true,
                DueText = "Entrega: Mañana 23:59",
                Category = PostTypeCategory.Assignment
            },
            new FeedPostItem
            {
                Title = "Nuevo material: Criterios de Evaluación y Temario",
                Subtitle = "5IM6 Derecho Mercantil • 3 Sep",
                Icon = "📖",
                HasDueDate = false,
                DueText = string.Empty,
                Category = PostTypeCategory.Material
            },
            new FeedPostItem
            {
                Title = "Aviso: Revisión del Avance del Proyecto WorkFlow",
                Subtitle = "Modelado de Sistemas • Hace 2 días",
                Icon = "📢",
                HasDueDate = false,
                DueText = string.Empty,
                Category = PostTypeCategory.Announcement
            }
        };
    }

    private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is CourseItem selectedCourse)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new CourseDetailPage(selectedCourse));
        }
    }

    private async void OnRecentPostTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is FeedPostItem post)
        {
            if (post.HasDueDate)
            {
                var assignment = new AssignmentItem
                {
                    Title = post.Title,
                    DueDateText = post.DueText,
                    StatusText = "Asignada",
                    StatusBadgeBackground = Color.FromArgb("#162D34"),
                    StatusTextColor = Color.FromArgb("#5EEAD4")
                };

                await Navigation.PushAsync(new TaskDetailPage(assignment));
            }
            else
            {
                await DisplayAlert("Aviso / Material", post.Title, "Aceptar");
            }
        }
    }

    private async void OnAddClassClicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet(
            "WorkFlow - Gestión de Clases",
            "Cancelar",
            null,
            "Unirse a una clase",
            "Crear una clase");

        if (action == "Unirse a una clase")
        {
            await DisplayAlert("WorkFlow", "Abrir modal para unirse con código de clase.", "Aceptar");
        }
        else if (action == "Crear una clase")
        {
            await DisplayAlert("WorkFlow", "Abrir modal para crear nueva clase.", "Aceptar");
        }
    }
}