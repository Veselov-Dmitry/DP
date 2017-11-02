using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Services.Interface
{
    //
    // Сводка:
    //     Определяет методы, упрощающие поиск служб и разрешение зависимостей.
    public interface IDependencyResolver
    {
        //
        // Сводка:
        //     Разрешает однократно зарегистрированные службы, поддерживающие создание произвольных
        //     объектов.
        //
        // Параметры:
        //   serviceType:
        //     Тип запрошенной службы или объекта.
        //
        // Возврат:
        //     Запрошенная служба или объект.
        object GetService(Type serviceType);
        //
        // Сводка:
        //     Разрешает многократно зарегистрированные службы.
        //
        // Параметры:
        //   serviceType:
        //     Тип запрашиваемых служб.
        //
        // Возврат:
        //     Запрошенные службы.
        IEnumerable<object> GetServices(Type serviceType);
    }
}
