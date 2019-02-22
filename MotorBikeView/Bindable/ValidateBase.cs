using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MotorBikeView.Bindable
{
    internal class ValidateBase : BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        public bool HasErrors => _errors.Count > 0;

        public IEnumerable GetErrors(string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                return null;
            }

            return _errors[propertyName];
        }

        protected override void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            ValidateProperty(propertyName, val);
            base.SetProperty(ref member, val, propertyName);
        }

        private void ValidateProperty<T>(string propertyName, T newValue)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(this);
            context.MemberName = propertyName;

            Validator.TryValidateProperty(newValue, context, result);

            if (result.Any())
            {
                _errors[propertyName] = result.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                _errors.Remove(propertyName);
            }

            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

    }
}
