using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.PresentationModel
{
    public abstract class ViewModel : ObservableObject, INotifyDataErrorInfo, IDataErrorInfo
    {
        private ModelState state;
        public ModelState State
        {
            get { return state; }
            internal protected set
            {
                SetProperty(ref state, value);
            }
        }

        public override void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            //

            if (State == ModelState.Unchanged && propertyName != "State" && propertyName != "OriginalVersion")
            {                
                State = ModelState.Modified;
            }
        }

        public void SetAsModified()
        {
            State = ModelState.Modified;
        }

        public virtual void OnSave()
        {
            State = ModelState.Unchanged;
        }

        protected readonly Dictionary<string, IList<Exception>> validationErrors = new Dictionary<string, IList<Exception>>();

        protected void ClearErrors(string propertyName)
        {
            if (!validationErrors.ContainsKey(propertyName))
            {
                return;
            }

            validationErrors.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        protected void RaiseErrorsChanged(string propertyName, Exception exception)
        {
            IList<Exception> errors;

            if (validationErrors.ContainsKey(propertyName))
            {
                errors = validationErrors[propertyName];
            }
            else
            {
                errors = new List<Exception>();

                validationErrors.Add(propertyName, errors);
            }

            errors.Add(exception);

            OnErrorsChanged(propertyName);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public bool HasErrors
        {
            get { return !string.IsNullOrEmpty(Error) || validationErrors.Count > 0; }
        }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (validationErrors.Count == 0)
                {
                    return null;
                }

                if (validationErrors[columnName].Count > 0)
                {
                    return validationErrors[columnName][0].Message;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !validationErrors.ContainsKey(propertyName))
            {
                return null;
            }

            return validationErrors[propertyName];
        }
    }
}
