﻿namespace ParamSharper.Flags;

public enum ParamVariableType : byte
{
    String, Int, Double, Float, Array, Expression, Directive, Custom = Array,
}