using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
    public class FilterServices
    {
        /// <summary>
        /// Фильтрует и сортирует список логов по заданным параметрам.
        /// </summary>
        public List<ParsedLog> ApplyFilters(
            IEnumerable<ParsedLog> logs,
            string? searchPrompt = null,
            bool showHidden = false,
            bool partialComparing = false,
            string? filename = null,
            string? sessionName = null,
            int? pivot = null,
            int? page = null,
            int? logsPerPage = null)
        {
            var query = logs.AsQueryable();

            // Фильтр по скрытым
            if (!showHidden)
                query = query.Where(x => !x.IsHidden);

            // Фильтр по поисковой строке
            if (!string.IsNullOrWhiteSpace(searchPrompt))
            {
                if (partialComparing)
                    query = query.Where(x => x.Message?.Contains(searchPrompt, StringComparison.OrdinalIgnoreCase) == true);
                else
                    query = query.Where(x => string.Equals(x.Message, searchPrompt, StringComparison.OrdinalIgnoreCase));
            }

            // Фильтр по имени файла
            if (!string.IsNullOrWhiteSpace(filename))
                query = query.Where(x => string.Equals(x.Filename, filename, StringComparison.OrdinalIgnoreCase));

            // Фильтр по имени сессии
            if (!string.IsNullOrWhiteSpace(sessionName))
                query = query.Where(x => string.Equals(x.SessionName, sessionName, StringComparison.OrdinalIgnoreCase));

            // Сортировка по времени (пример)
            query = query.OrderByDescending(x => x.Timestamp);

            // Фильтр по уровню лога
            if (levels != null && levels.Any())
                query = query.Where(x => levels.Contains(x.Level, StringComparer.OrdinalIgnoreCase));

            // Пагинация
            if (page.HasValue && logsPerPage.HasValue && logsPerPage.Value > 0)
            {
                int skip = ((page.Value - 1) * logsPerPage.Value) + (pivot ?? 0);
                query = query.Skip(skip).Take(logsPerPage.Value);
            }

            return query.ToList();
        }
    }
}
