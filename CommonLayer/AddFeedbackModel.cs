﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class AddFeedbackModel
    {
        public float Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}
