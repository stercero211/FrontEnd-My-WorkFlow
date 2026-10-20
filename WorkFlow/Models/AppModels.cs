using System.Collections.ObjectModel;
using System.Collections;

namespace WorkFlow.Models;

public enum PostTypeCategory
{
    Announcement, // Anuncio
    Assignment,   // Tarea
    Material      // Material de estudio
}

public class CourseItem
{
    public string Group { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Teacher { get; set; } = string.Empty;
    public string ClassCode { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

public class FeedPostItem
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = "📢";
    public bool HasDueDate { get; set; }
    public string DueText { get; set; } = string.Empty;
    public PostTypeCategory Category { get; set; } = PostTypeCategory.Announcement;

    public LinearGradientBrush CardGradientBrush => GetGradient(Category);

    public static LinearGradientBrush GetGradient(PostTypeCategory category)
    {
        Color topColor = category switch
        {
            PostTypeCategory.Assignment => Color.FromArgb("#35F87171"), // Rojo/Coral Tareas
            PostTypeCategory.Material => Color.FromArgb("#355C93F9"), // Azul Materiales
            _ => Color.FromArgb("#355EEAD4")  // Menta/Teal Anuncios
        };

        return new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0, 1),
            GradientStops = new GradientStopCollection
            {
                new GradientStop(topColor, 0.0f),
                new GradientStop(Colors.Transparent, 0.85f)
            }
        };
    }
}

public class ClassroomPostModel
{
    public string Title { get; set; } = string.Empty;
    public string DateFormatted { get; set; } = string.Empty;
    public string IconSymbol { get; set; } = "📢";
    public Color IconBgColor { get; set; } = Colors.Gray;
    public bool IsAssignment { get; set; }
    public string DueDateText { get; set; } = string.Empty;
    public string CommentsCountText { get; set; } = string.Empty;
    public PostTypeCategory Category { get; set; } = PostTypeCategory.Announcement;

    public LinearGradientBrush CardGradientBrush => FeedPostItem.GetGradient(Category);
}

public class AssignmentUnitGroup
{
    public string UnitTitle { get; set; } = string.Empty;
    public string ProgressSummary { get; set; } = string.Empty;
    public ObservableCollection<AssignmentItem> Assignments { get; set; } = new();
}

public class AssignmentItem
{
    public string Title { get; set; } = string.Empty;
    public string DueDateText { get; set; } = string.Empty;
    public string IconSymbol { get; set; } = "📋";
    public Color IconBackground { get; set; } = Colors.Gray;
    public string StatusText { get; set; } = string.Empty;
    public Color StatusBadgeBackground { get; set; } = Colors.Transparent;
    public Color StatusTextColor { get; set; } = Colors.White;
    public PostTypeCategory Category { get; set; } = PostTypeCategory.Assignment;

    public LinearGradientBrush CardGradientBrush => FeedPostItem.GetGradient(Category);
}

public class PersonModel
{
    public string FullName { get; set; } = string.Empty;
    public string SubtitleRole { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = "avatar_user.png";
}

public class StudentFileModel
{
    public string FileName { get; set; } = string.Empty;
    public string FileSizeText { get; set; } = string.Empty;
    public string FileIcon { get; set; } = "📄";
    public string FilePath { get; set; } = string.Empty;
}

public class PrivateCommentModel
{
    public string AuthorName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TimeAgo { get; set; } = string.Empty;
}