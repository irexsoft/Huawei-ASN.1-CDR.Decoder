using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Decoder
{
    /// 
    /// "DynamicProperties for PropertyGrid" By Mizan Rahman
    /// More info: http://www.codeproject.com/KB/grid/PropertyGridDynamicProp.aspx 
    /// 
    class DynamicTypeDescriptionProvider : TypeDescriptionProvider
    {
        private TypeDescriptionProvider _parent = null;

        /// <summary>
        /// A user of TypeDescriptor will usually invoke this constructor.
        /// But this constructor has no way of knowing the parent TypeDescriptor or 
        /// object it is attached to.  To overcome this problem, we do some tricks 
        /// in GetTypeDescriptor method
        /// </summary>
        public DynamicTypeDescriptionProvider()
            : base()
        {
        }

        /// <summary>
        /// This constructor is the one gives use enough information to 
        /// get type and property descriport data
        /// </summary>
        /// <param name="parent"></param>
        public DynamicTypeDescriptionProvider(TypeDescriptionProvider parent)
            : base(parent)
        {
            _parent = parent;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            if (_parent == null)  // we need to get the parent now
            {
                //NOTE:  we must use objectType to get the parent, and not the instance
                // we pop ourself from the stack
                TypeDescriptor.RemoveProvider(this, objectType);  // this will allow us to access the previous provider

                // we get the previous provider provided by somebody else (likely by .NET framework as default provider)
                _parent = TypeDescriptor.GetProvider(objectType);

                // now push ourself back onto the stack
                TypeDescriptor.AddProvider(this, objectType);
            }

            if (instance != null)
            {
                return new DynamicCustomTypeDescriptor(_parent.GetTypeDescriptor(instance), instance);
            }
            else
            {
                return _parent.GetTypeDescriptor(objectType);
            }
        }
    }

    class DynamicCustomTypeDescriptor : CustomTypeDescriptor
    {
        private PropertyDescriptorCollection _pdcFiltered = null;
        private PropertyDescriptorCollection _pdcUnFiltered = null;
        private object _instance = null;
        private MethodInfo _methodInfo = null;

        public DynamicCustomTypeDescriptor(ICustomTypeDescriptor ctd, object instance)
            : base(ctd)
        {
            _instance = instance;

            // define the parameter data type of the method
            Type[] arrType = new Type[1];
            arrType.SetValue(typeof(PropertyDescriptorCollection), 0);

            // get method info
            _methodInfo = _instance.GetType().GetMethod("ModifyDynamicProperties", arrType);
        }

        public override object GetPropertyOwner(PropertyDescriptor pd)
        {
            return _instance; //base.GetPropertyOwner(pd);
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            if (_pdcUnFiltered == null)
            {
                PropertyDescriptorCollection pdc = base.GetProperties();  // this gives us a readonly collection, no good
                Array arr = Array.CreateInstance(typeof(PropertyDescriptor), pdc.Count);  // we construct a new collection so we can do add/remove
                pdc.CopyTo(arr, 0);
                _pdcUnFiltered = new PropertyDescriptorCollection((PropertyDescriptor[])arr);
            }
            if (_methodInfo != null)
            {
                // invoke the method
                Object[] arrObj = { _pdcUnFiltered };

                object obj = _methodInfo.Invoke(_instance, arrObj);
            }

            return _pdcUnFiltered;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (_pdcFiltered == null)
            {
                PropertyDescriptorCollection pdc = base.GetProperties(attributes);  // this gives us a readonly collection, no good
                Array arr = Array.CreateInstance(typeof(PropertyDescriptor), pdc.Count);
                pdc.CopyTo(arr, 0);
                _pdcFiltered = new PropertyDescriptorCollection((PropertyDescriptor[])arr);
            }
            if (_methodInfo != null)
            {
                // invoke the method
                Object[] arrObj = { _pdcFiltered };

                object obj = _methodInfo.Invoke(_instance, arrObj);

            }
            return _pdcFiltered;
        }
    }
}
