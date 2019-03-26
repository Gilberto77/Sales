using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Interfaces
{
    using System.Globalization;

    public interface ILocalize
    {

        // para leer la informacion del telefono idioma
   
        CultureInfo GetCurrentCultureInfo();

        // para modificar la informacion del telefono idioma
        void SetLocale(CultureInfo ci);
    }
}
