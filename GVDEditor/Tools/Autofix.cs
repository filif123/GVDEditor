using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GVDEditor.Forms;

namespace GVDEditor.Tools
{
    //TODO urobit nieco s Autofix funkciou

    public interface IFixable
    {

    }

    public enum AutofixOutputType
    {
        TRAIN_TYPE,
        C
    }

    internal class Autofix
    {
        /// <summary>Initializes a new instance of the <see cref="T:Autofix" /> class.</summary>
        public Autofix(AutofixOutputType type, string errorText, IEnumerable<IFixable> selection, string wrongInput = "")
        {
            Type = type;
            ErrorText = errorText;
            Selection = selection;
            WrongInput = wrongInput;
        }

        public string WrongInput { get; }

        public AutofixOutputType Type { get; }

        public string ErrorText { get; }

        public IEnumerable<IFixable> Selection { get; }

        public IFixable Result { get; private set; }

        public bool ShowAutofix(Form parent)
        {
            bool result = false;
            parent.BeginInvoke(new Action(() =>
            {
                var form = new FAutofix(Type, ErrorText, Selection, WrongInput);
                form.ShowDialog();
                switch (form.Result)
                {
                    case AutofixResult.NONE:
                        result = false;
                        break;
                    case AutofixResult.CREATE_NEW:
                    case AutofixResult.SELECTION:
                        Result = form.ResultItem;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                result = false;
            })).AsyncWaitHandle.WaitOne();

            return result;
        }
    }
}