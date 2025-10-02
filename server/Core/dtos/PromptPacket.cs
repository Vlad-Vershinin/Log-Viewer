
/*
 TODO: Добавить свойства для фильтров,
после чего реализовать их в LogsRepository.
 */

public class PromptPacket
{
    public string SessionName { get; set; } // Идентификационное имя сессии
    public string Filename { get; set; } // Имя выбранного в данный момент файла
    public int Pivot { get; set; } // Смещение по отображаемому списку логов
    public int Page { get; set; } // Запрашиваемая страница
    public int LogsPerPage { get; set; } // Количество логов на одну страницу datagrid
    public bool ShowHidden { get; set; } // Показать скрытые логи
    public string SearchPrompt { get; set; } // Всетекстовой поиск по частичному совпадению с этим промптом
}