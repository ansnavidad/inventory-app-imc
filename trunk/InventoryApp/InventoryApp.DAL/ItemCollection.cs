﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace InventoryApp.DAL
{
    public class ItemCollection : ObservableCollection<Item>
    {
        public ItemCollection() { }
    }
}