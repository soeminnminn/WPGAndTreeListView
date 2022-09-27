using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Demo
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Collection = new ObservableCollection<TestData>
            {
                new TestData
                {
                    Name = "Être",
                    Category = TestCategory.None,
                    Description = "If you be there",
                    GarconLevel = 18,
                    Children = new List<TestData>
                    {
                        new TestData
                        {
                            Name = "suis",
                            Category = TestCategory.Je,
                            GarconLevel = 13,
                            Description = "If I am there"
                        },
                        new TestData
                        {
                            Name = "es",
                            Category = TestCategory.Tu,
                            GarconLevel = 13,
                            Description = "If you are there"
                        },
                        new TestData
                        {
                            Name = "Multiple Options",
                            Category = TestCategory.None,
                            Description = "",
                            GarconLevel = -1,
                            Children = new List<TestData>
                            {
                                new TestData
                                {
                                    Name = "est",
                                    Category = TestCategory.Il,
                                    GarconLevel = 198,
                                    Description = "If he is there"
                                },
                                new TestData
                                {
                                    Name = "est",
                                    Category = TestCategory.Elle,
                                    GarconLevel = 198,
                                    Description = "If she is there"
                                },
                                new TestData
                                {
                                    Name = "est",
                                    Category = TestCategory.On,
                                    GarconLevel = 198,
                                    Description = "If he it there"
                                }
                            }
                        }
                    }
                }
            };

            //for (int i = 0; i < 1000; i++)
            //{
            //    Collection.Add(new TestData {Name = "Virtualizing Test"});
            //}

            TestProperties = new TestObject()
            {
                DoubleValue = 77.0,
                IntValue = 56789,
                FloatValue = -15.1f,
                StrValue = "Nicolas",
                BoolValue = true,
                ColorValue = Colors.Yellow,
                EnumValue = TestCategory.On
            };
        }

        public ObservableCollection<TestData> Collection { get; }

        public TestObject TestProperties { get; }
    }

    public class TestData
    {
        public string Name { get; set; }
        public TestCategory Category { get; set; }
        public string Description { get; set; }
        public int GarconLevel { get; set; }
        public bool IsExpanded { get; set; }
        public List<TestData> Children { get; set; }
    }

    public enum TestCategory
    {
        None,
        Je,
        Tu,
        Il,
        Elle,
        On,
        Nous,
        Vous,
        IlsElles
    }

    public class TestObject : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// This field stores a double value.
        /// </summary>
        private double mDoubleValue;

        /// <summary>
        /// This field stores a integer value.
        /// </summary>
        private int mIntValue;

        /// <summary>
        /// This field stores a string value.
        /// </summary>
        private string mStrValue;

        /// <summary>
        /// The field stores a float value.
        /// </summary>
        private float mFloatValue;

        /// <summary>
        /// This field stores a boolean value.
        /// </summary>
        private bool mBoolValue;

        /// <summary>
        /// This field stores a color value.
        /// </summary>
        private Color mColorValue;

        /// <summary>
        /// This field stores a date/time value.
        /// </summary>
        private DateTime mDateTimeValue;

        private TestCategory mEnumValue;

        #endregion

        #region Events

        /// <summary>
        /// This event is called each time a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = null;

        #endregion

        #region Constructor
        public TestObject()
        { }
        #endregion

        #region Methods
        /// <summary>
        /// This method raises an event when a property is changed./
        /// </summary>
        /// <param name="pPropertyName">The property name which has changed.</param>
        private void RaisePropertyChanged(string pPropertyName)
        {
            if
                (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pPropertyName));
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the double value.
        /// </summary>
        public double DoubleValue
        {
            get
            {
                return mDoubleValue;
            }
            set
            {
                mDoubleValue = value;
                this.RaisePropertyChanged("DoubleValue");
            }
        }

        /// <summary>
        /// Gets or sets the integer value.
        /// </summary>
        public int IntValue
        {
            get
            {
                return mIntValue;
            }
            set
            {
                mIntValue = value;
                this.RaisePropertyChanged("IntValue");
            }
        }

        /// <summary>
        /// Gets or sets the float value.
        /// </summary>
        public float FloatValue
        {
            get
            {
                return mFloatValue;
            }
            set
            {
                mFloatValue = value;
                this.RaisePropertyChanged("FloatValue");
            }
        }

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public string StrValue
        {
            get
            {
                return mStrValue;
            }
            set
            {
                mStrValue = value;
                this.RaisePropertyChanged("StrValue");
            }
        }

        /// <summary>
        /// Gets or sets the boolean value.
        /// </summary>
        public bool BoolValue
        {
            get
            {
                return mBoolValue;
            }
            set
            {
                mBoolValue = value;
                this.RaisePropertyChanged("BoolValue");
            }
        }

        /// <summary>
        /// Gets or sets the color value.
        /// </summary>
        public Color ColorValue
        {
            get
            {
                return mColorValue;
            }
            set
            {
                mColorValue = value;
                this.RaisePropertyChanged("ColorValue");
            }
        }

        /// <summary>
        /// Gets or sets the date/time value.
        /// </summary>
        public DateTime DateTimeValue
        {
            get
            {
                return mDateTimeValue;
            }
            set
            {
                mDateTimeValue = value;
                this.RaisePropertyChanged("DateTimeValue");
            }
        }


        public TestCategory EnumValue
        {
            get => mEnumValue;
            set 
            { 
                mEnumValue = value;
                this.RaisePropertyChanged("EnumValue");
            }
        }
        #endregion

    }
}