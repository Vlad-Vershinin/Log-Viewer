using client.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
    public static class Readable
    {
        public static T MarkAsRead<T>(this T item) where T : IReadable
        {
            item.IsRead = true;
            item.ReadDate = DateTime.Now;
            return item;
        }

        public static async Task<T> MarkAsReadAsync<T>(this T item) where T : IReadable
        {
            item.IsRead = true;
            item.ReadDate = DateTime.Now;

            // тут можно сохранить это состояние в бд
            await Task.CompletedTask;

            return item;
        }


        public static IEnumerable<T> MarkAllAsRead<T>(this IEnumerable<T> items)
        where T : IReadable
        {
            foreach (var item in items)
            {
                item.MarkAsRead();
            }
            return items;
        }
    }
}
