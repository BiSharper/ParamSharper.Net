namespace ParamSharper.Flags;

public enum ParamAccessibility : byte
{
    Default, //Default - Read Write
    ReadCreate, //Only allow adding new class members
    ReadOnly,
    ReadOnlyVerified, //Apply CRC Test
}