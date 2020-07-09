using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace App.CustomValidation
{
    public class CantidadMinimaElementosAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        public CantidadMinimaElementosAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= _minElements;
            }
            return false;
            //return base.IsValid(value);
        }
    }
}
