using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace WebFormMapper
{
    public sealed class Mapper<T> where T : class, new()
    {
        private T _model;
        public Dictionary<Control, object> ControlsFormat { get; private set; }
        public Dictionary<Control, object> ControlsFormatStyle { get; private set; }
        public Dictionary<Control, string> ControlsToMap { get; private set; }
        public NumberStyles NumberStyle { get; set; }
        public DateTimeStyles DateTimeStyle { get; set; }

        public Mapper(T model)
        {
            _model = model;
            ControlsFormat = new Dictionary<Control, object>();
            ControlsToMap = new Dictionary<Control, string>();
        }

        public void AddMapping(Control control, string modelAttribute)
        {
            ControlsToMap.Add(control, modelAttribute);
        }

        public void RemoveMapping(Control control)
        {
            ControlsToMap.Remove(control);
        }

        public void AddFormatting(Control control, object format)
        {
            ControlsFormat.Add(control, format);
        }

        public void RemoveFormatting(Control control)
        {
            ControlsFormat.Remove(control);
        }

        public void MapToControls()
        {
            foreach (var control in ControlsToMap)
            {
                IFormatProvider format = ControlsFormat.ContainsKey(control.Key) ? ControlsFormat[control.Key] is IFormatProvider ? (IFormatProvider)ControlsFormat[control.Key] : null : null;
                string formatString = ControlsFormat.ContainsKey(control.Key) ? ControlsFormat[control.Key] is String ? (String)ControlsFormat[control.Key] : null : null;
                if (control.Key is ListControl)
                {
                    if (format != null)
                    {
                        ((ListControl)control.Key).SelectedValue = Converter.ToString(_model.GetType().GetProperty(control.Value).GetValue(_model, null), format);
                    }
                    else if (formatString != null)
                    {
                        ((ListControl)control.Key).SelectedValue = Converter.ToString(_model.GetType().GetProperty(control.Value).GetValue(_model, null), formatString);
                    }
                    else
                    {
                        ((ListControl)control.Key).SelectedValue = Converter.ToString(_model.GetType().GetProperty(control.Value).GetValue(_model, null));
                    }
                    continue;
                }
                if (control.Key is ITextControl)
                {
                    if (format != null)
                    {
                        ((ITextControl)control.Key).Text = Converter.ToString(_model.GetType().GetProperty(control.Value).GetValue(_model, null), format);
                    }
                    else if (formatString != null)
                    {
                        ((ITextControl)control.Key).Text = Converter.ToString(_model.GetType().GetProperty(control.Value).GetValue(_model, null), formatString);
                    }
                    else
                    {
                        ((ITextControl)control.Key).Text = Converter.ToString(_model.GetType().GetProperty(control.Value).GetValue(_model, null));
                    }
                    continue;
                }
            }
        }

        public T MapToModel()
        {
            foreach (var control in ControlsToMap)
            {
                IFormatProvider format = ControlsFormat.ContainsKey(control.Key) ? ControlsFormat[control.Key] is IFormatProvider ? (IFormatProvider)ControlsFormat[control.Key] : null : null;
                String formatString = ControlsFormat.ContainsKey(control.Key) ? ControlsFormat[control.Key] is String ? (String)ControlsFormat[control.Key] : null : null;
                if (control.Key is ListControl)
                {
                    if (format != null)
                    {
                        _model.GetType().GetProperty(control.Value).SetValue(_model, Converter.Parse(((ListControl)control.Key).SelectedValue, _model.GetType().GetProperty(control.Value).PropertyType, format), null);
                    }
                    else if (formatString != null)
                    {
                        _model.GetType().GetProperty(control.Value).SetValue(_model, Converter.Parse(((ListControl)control.Key).SelectedValue, _model.GetType().GetProperty(control.Value).PropertyType, formatString), null);
                    }
                    else
                    {
                        _model.GetType().GetProperty(control.Value).SetValue(_model, Converter.Parse(((ListControl)control.Key).SelectedValue, _model.GetType().GetProperty(control.Value).PropertyType), null);
                    }
                    continue;
                }
                if (control.Key is ITextControl)
                {
                    if (format != null)
                    {
                        _model.GetType().GetProperty(control.Value).SetValue(_model, Converter.Parse(((ITextControl)control.Key).Text, _model.GetType().GetProperty(control.Value).PropertyType, format), null);
                    }
                    else if (formatString != null)
                    {
                        _model.GetType().GetProperty(control.Value).SetValue(_model, Converter.Parse(((ITextControl)control.Key).Text, _model.GetType().GetProperty(control.Value).PropertyType, formatString), null);
                    }
                    else
                    {
                        _model.GetType().GetProperty(control.Value).SetValue(_model, Converter.Parse(((ITextControl)control.Key).Text, _model.GetType().GetProperty(control.Value).PropertyType), null);
                    }
                    continue;
                }
            }
            return _model;
        }
    }

    public sealed class Converter
    {
        public static object Parse(string input, Type output)
        {
            System.Reflection.MethodInfo method = null;
            method = output.GetMethod("Parse", new Type[1] { typeof(string) });
            if (method != null)
            {
                return method.Invoke(method, new object[1] { input });
            }
            else
            {
                return input.ToString();
            }
        }

        public static object Parse(string input, Type output, IFormatProvider format)
        {
            System.Reflection.MethodInfo method = null;
            if (format != null)
            {
                method = output.GetMethod("Parse", new Type[2] { typeof(string), typeof(IFormatProvider) });
            }
            else
            {
                method = output.GetMethod("Parse", new Type[1] { typeof(string) });
            }
            if (method != null)
            {
                if (format != null)
                {
                    return method.Invoke(method, new object[2] { input, format });
                }
                else
                {
                    return method.Invoke(method, new object[1] { input });
                }
            }
            else
            {
                return input.ToString();
            }
        }

        public static object Parse(string input, Type output, String format)
        {
            System.Reflection.MethodInfo method = null;
            if (format != null)
            {
                method = output.GetMethod("Parse", new Type[2] { typeof(string), typeof(String) });
            }
            else
            {
                method = output.GetMethod("Parse", new Type[1] { typeof(string) });
            }
            if (method != null)
            {
                if (format != null)
                {
                    return method.Invoke(method, new object[2] { input, format });
                }
                else
                {
                    return method.Invoke(method, new object[1] { input });
                }
            }
            else
            {
                method = output.GetMethod("Parse", new Type[1] { typeof(string) });
                return method.Invoke(method, new object[1] { input });
            }
        }

        public static string ToString(object input)
        {
            return input != null ? input.ToString() : string.Empty;
        }

        public static string ToString(object input, IFormatProvider format)
        {
            var method = input.GetType().GetMethod("ToString", new Type[1] { typeof(IFormatProvider) });
            if (method != null)
            {
                if (format != null)
                {
                    return method.Invoke(input, new object[1] { format }).ToString();
                }
                else
                {
                    return input.ToString();
                }
            }
            else
            {
                return input.ToString();
            }
        }

        public static string ToString(object input, string format)
        {
            var method = input.GetType().GetMethod("ToString", new Type[1] { typeof(String) });
            if (method != null)
            {
                if (format != null)
                {
                    return method.Invoke(input, new object[1] { format }).ToString();
                }
                else
                {
                    return input.ToString();
                }
            }
            else
            {
                return input.ToString();
            }
        }
    }
}
