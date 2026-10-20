using System.Collections.ObjectModel;
using WorkFlow.Models;

namespace WorkFlow;

public partial class CourseDetailPage : ContentPage
{
    public CourseItem Course { get; set; }
    public ObservableCollection<ClassroomPostModel> FeedPosts { get; set; }
    public ObservableCollection<AssignmentUnitGroup> AssignmentUnits { get; set; }
    public ObservableCollection<PersonModel> Teachers { get; set; }
    public ObservableCollection<PersonModel> Students { get; set; }
    public string StudentsCountText => $"{Students?.Count ?? 0} alumnos";

    public CourseDetailPage() : this(new CourseItem
    {
        Group = "5IM6",
        Title = "DERECHO MERCANTIL",
        Teacher = "Prof. Alejandro Cruz",
        ClassCode = "WF5M6M",
        ImageUrl = "https://images.unsplash.com/photo-1589829545856-d10d557cf95f?auto=format&fit=crop&w=800&q=80"
    })
    {
    }

    public CourseDetailPage(CourseItem selectedCourse)
    {
        InitializeComponent();
        Course = selectedCourse;

        LoadNovedadesData();
        LoadTrabajosData();
        LoadPersonasData();

        BindingContext = this;
    }

    private void LoadNovedadesData()
    {
        FeedPosts = new ObservableCollection<ClassroomPostModel>();

        if (Course.ClassCode == "WF5M6S") // MODELADO DE SISTEMAS
        {
            FeedPosts.Add(new ClassroomPostModel
            {
                Title = "Aviso: Entregables del Diagrama de Casos de Uso",
                DateFormatted = "Hace 2 días",
                IconSymbol = "📢",
                IconBgColor = Color.FromArgb("#242B35"),
                IsAssignment = false,
                CommentsCountText = "3 comentarios",
                Category = PostTypeCategory.Announcement
            });
            FeedPosts.Add(new ClassroomPostModel
            {
                Title = "Nueva tarea: Avance 2 - Diagrama de Clases UML",
                DateFormatted = "Ayer",
                IconSymbol = "📋",
                IconBgColor = Color.FromArgb("#1A3A4B"),
                IsAssignment = true,
                DueDateText = "Fecha de entrega: Viernes 23:59",
                CommentsCountText = "Agregar un comentario",
                Category = PostTypeCategory.Assignment
            });
        }
        else if (Course.ClassCode == "WF5M6P") // DESARROLLO MÓVIL .NET MAUI
        {
            FeedPosts.Add(new ClassroomPostModel
            {
                Title = "Nuevo material: Guía de XAML y DataBinding",
                DateFormatted = "12 de feb.",
                IconSymbol = "📖",
                IconBgColor = Color.FromArgb("#162D34"),
                IsAssignment = false,
                CommentsCountText = "Sin comentarios",
                Category = PostTypeCategory.Material
            });
            FeedPosts.Add(new ClassroomPostModel
            {
                Title = "Nueva tarea: Práctica 3 - Navegación entre Pantallas",
                DateFormatted = "Hoy",
                IconSymbol = "📋",
                IconBgColor = Color.FromArgb("#1A3A4B"),
                IsAssignment = true,
                DueDateText = "Fecha de entrega: Próximo lunes",
                CommentsCountText = "1 comentario de clase",
                Category = PostTypeCategory.Assignment
            });
        }
        else // DERECHO MERCANTIL
        {
            FeedPosts.Add(new ClassroomPostModel
            {
                Title = "Nuevo material: Criterios de Evaluación y Temario",
                DateFormatted = "3 de feb.",
                IconSymbol = "📖",
                IconBgColor = Color.FromArgb("#162D34"),
                IsAssignment = false,
                CommentsCountText = "2 comentarios de clase",
                Category = PostTypeCategory.Material
            });
            FeedPosts.Add(new ClassroomPostModel
            {
                Title = "Nueva tarea: 1. Actividad de apertura. Marco Jurídico",
                DateFormatted = "5 de feb.",
                IconSymbol = "📋",
                IconBgColor = Color.FromArgb("#1A3A4B"),
                IsAssignment = true,
                DueDateText = "Fecha de entrega: Mañana 23:59",
                CommentsCountText = "Agregar un comentario de clase",
                Category = PostTypeCategory.Assignment
            });
        }
    }

    private void LoadTrabajosData()
    {
        AssignmentUnits = new ObservableCollection<AssignmentUnitGroup>();

        if (Course.ClassCode == "WF5M6S")
        {
            AssignmentUnits.Add(new AssignmentUnitGroup
            {
                UnitTitle = "Unidad 1: Análisis y Requerimientos",
                ProgressSummary = "1 de 2 entregadas",
                Assignments = new ObservableCollection<AssignmentItem>
                {
                    new AssignmentItem
                    {
                        Title = "Avance 1: Especificación de Requerimientos IEEE 830",
                        DueDateText = "Entregado • 100 pts",
                        IconSymbol = "📋",
                        IconBackground = Color.FromArgb("#1A3A4B"),
                        StatusText = "Entregada",
                        StatusBadgeBackground = Color.FromArgb("#14382A"),
                        StatusTextColor = Color.FromArgb("#86EFAC"),
                        Category = PostTypeCategory.Assignment
                    },
                    new AssignmentItem
                    {
                        Title = "Avance 2 - Diagrama de Clases UML",
                        DueDateText = "Vence: Viernes 23:59 • 100 pts",
                        IconSymbol = "📋",
                        IconBackground = Color.FromArgb("#1A3A4B"),
                        StatusText = "Asignada",
                        StatusBadgeBackground = Color.FromArgb("#162D34"),
                        StatusTextColor = Color.FromArgb("#5EEAD4"),
                        Category = PostTypeCategory.Assignment
                    }
                }
            });
        }
        else if (Course.ClassCode == "WF5M6P")
        {
            AssignmentUnits.Add(new AssignmentUnitGroup
            {
                UnitTitle = "Unidad 1: Fundamentos de MAUI y XAML",
                ProgressSummary = "0 de 1 entregadas",
                Assignments = new ObservableCollection<AssignmentItem>
                {
                    new AssignmentItem
                    {
                        Title = "Práctica 3 - Navegación entre Pantallas",
                        DueDateText = "Vence: Próximo lunes • 100 pts",
                        IconSymbol = "📋",
                        IconBackground = Color.FromArgb("#1A3A4B"),
                        StatusText = "Asignada",
                        StatusBadgeBackground = Color.FromArgb("#162D34"),
                        StatusTextColor = Color.FromArgb("#5EEAD4"),
                        Category = PostTypeCategory.Assignment
                    }
                }
            });
        }
        else
        {
            AssignmentUnits.Add(new AssignmentUnitGroup
            {
                UnitTitle = "Unidad 1: Marco Jurídico del Comercio",
                ProgressSummary = "2 de 3 entregadas",
                Assignments = new ObservableCollection<AssignmentItem>
                {
                    new AssignmentItem
                    {
                        Title = "1. Actividad de apertura: Marco Jurídico",
                        DueDateText = "Vence: Mañana 23:59 • 100 pts",
                        IconSymbol = "📋",
                        IconBackground = Color.FromArgb("#1A3A4B"),
                        StatusText = "Asignada",
                        StatusBadgeBackground = Color.FromArgb("#162D34"),
                        StatusTextColor = Color.FromArgb("#5EEAD4"),
                        Category = PostTypeCategory.Assignment
                    },
                    new AssignmentItem
                    {
                        Title = "2. Cuadro Comparativo: Actos de Comercio",
                        DueDateText = "Entregado el 7 de feb. • 100 pts",
                        IconSymbol = "📋",
                        IconBackground = Color.FromArgb("#1A3A4B"),
                        StatusText = "Entregada",
                        StatusBadgeBackground = Color.FromArgb("#14382A"),
                        StatusTextColor = Color.FromArgb("#86EFAC"),
                        Category = PostTypeCategory.Assignment
                    },
                    new AssignmentItem
                    {
                        Title = "Material: Código de Comercio (Art. 1 al 75)",
                        DueDateText = "Material de consulta • PDF",
                        IconSymbol = "📖",
                        IconBackground = Color.FromArgb("#162D34"),
                        StatusText = "Material",
                        StatusBadgeBackground = Color.FromArgb("#242B35"),
                        StatusTextColor = Color.FromArgb("#94A3B8"),
                        Category = PostTypeCategory.Material
                    }
                }
            });
        }
    }

    private void LoadPersonasData()
    {
        Teachers = new ObservableCollection<PersonModel>
        {
            new PersonModel
            {
                FullName = Course.Teacher,
                SubtitleRole = $"Docente Titular • {Course.ClassCode.ToLower()}@ipn.mx",
                AvatarUrl = "avatar_user.png"
            }
        };

        Students = new ObservableCollection<PersonModel>
        {
            new PersonModel
            {
                FullName = "Álvarez Morales Santiago",
                SubtitleRole = "Boleta: 2024130101 • Técnico en Informática",
                AvatarUrl = "avatar_user.png"
            },
            new PersonModel
            {
                FullName = "Castillo García Ximena",
                SubtitleRole = "Boleta: 2024130202 • Técnico en Informática",
                AvatarUrl = "avatar_user.png"
            },
            new PersonModel
            {
                FullName = "Hernández Pérez Diego",
                SubtitleRole = "Boleta: 2024130303 • Técnico en Informática",
                AvatarUrl = "avatar_user.png"
            }
        };
    }

    private void OnTabNovedadesTapped(object sender, TappedEventArgs e)
    {
        ViewNovedades.IsVisible = true;
        ViewTrabajos.IsVisible = false;
        ViewPersonas.IsVisible = false;

        TabNovedadesLabel.TextColor = Color.FromArgb("#5EEAD4");
        TabNovedadesIndicator.Color = Color.FromArgb("#5EEAD4");

        TabTrabajosLabel.TextColor = Color.FromArgb("#94A3B8");
        TabTrabajosIndicator.Color = Colors.Transparent;

        TabPersonasLabel.TextColor = Color.FromArgb("#94A3B8");
        TabPersonasIndicator.Color = Colors.Transparent;
    }

    private void OnTabTrabajosTapped(object sender, TappedEventArgs e)
    {
        ViewNovedades.IsVisible = false;
        ViewTrabajos.IsVisible = true;
        ViewPersonas.IsVisible = false;

        TabNovedadesLabel.TextColor = Color.FromArgb("#94A3B8");
        TabNovedadesIndicator.Color = Colors.Transparent;

        TabTrabajosLabel.TextColor = Color.FromArgb("#5EEAD4");
        TabTrabajosIndicator.Color = Color.FromArgb("#5EEAD4");

        TabPersonasLabel.TextColor = Color.FromArgb("#94A3B8");
        TabPersonasIndicator.Color = Colors.Transparent;
    }

    private void OnTabPersonasTapped(object sender, TappedEventArgs e)
    {
        ViewNovedades.IsVisible = false;
        ViewTrabajos.IsVisible = false;
        ViewPersonas.IsVisible = true;

        TabNovedadesLabel.TextColor = Color.FromArgb("#94A3B8");
        TabNovedadesIndicator.Color = Colors.Transparent;

        TabTrabajosLabel.TextColor = Color.FromArgb("#94A3B8");
        TabTrabajosIndicator.Color = Colors.Transparent;

        TabPersonasLabel.TextColor = Color.FromArgb("#5EEAD4");
        TabPersonasIndicator.Color = Color.FromArgb("#5EEAD4");
    }

    private async void OnFeedPostTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is ClassroomPostModel post)
        {
            if (post.IsAssignment)
            {
                var assignment = new AssignmentItem
                {
                    Title = post.Title,
                    DueDateText = post.DueDateText,
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

    private async void OnAssignmentTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is AssignmentItem selectedAssignment)
        {
            if (selectedAssignment.Category == PostTypeCategory.Material)
            {
                await DisplayAlert("Material de Estudio", selectedAssignment.Title, "Aceptar");
            }
            else
            {
                await Navigation.PushAsync(new TaskDetailPage(selectedAssignment));
            }
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnVideoCallClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Videollamada", "Iniciando sala virtual de la clase...", "Unirse");
    }

    private async void OnCourseInfoClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Información de la Clase",
            $"Materia: {Course.Title}\nGrupo: {Course.Group}\nDocente: {Course.Teacher}\nCódigo: {Course.ClassCode}",
            "Entendido");
    }

    private async void OnCreatePostTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("WorkFlow", "Abrir editor de publicación para anuncios.", "Continuar");
    }

    private async void OnPostOptionsClicked(object sender, EventArgs e)
    {
        await DisplayActionSheet("Opciones de publicación", "Cancelar", null, "Copiar enlace", "Reportar");
    }

    private async void OnFilterAssignmentsClicked(object sender, EventArgs e)
    {
        await DisplayActionSheet("Filtrar por unidad", "Cancelar", null, "Todas las unidades", "Unidad 1", "Unidad 2");
    }

    private async void OnEmailMemberClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Correo", "Abriendo cliente de correo institucional...", "Aceptar");
    }
}