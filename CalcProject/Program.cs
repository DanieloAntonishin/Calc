using System;
using CalcProject.Services;
RomanNumber.Resources =                        // Dependency Injection
    new CalcProject.Services.Resources();      //  via property

RomanNumber.Resources.SetCulture();            // input culture by user choice

var calc = new CalcProject.Services.Calc(     
    RomanNumber.Resources);                    //  via constructor

calc.Run();                                    // Run calculator 




// Hello from Github
// Hello form VS
