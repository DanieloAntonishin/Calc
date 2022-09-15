using System;
using CalcProject.Services;

RomanNumber.Resources =                   // Dependency Injection
    new CalcProject.Services.Resources();      //  via property
var calc = new CalcProject.Services.Calc(      // 
    RomanNumber.Resources);               //  via constructor

calc.Run();




// Hello from Github
// Hello form VS
