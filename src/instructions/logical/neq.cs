﻿namespace ancient.runtime
{
    public class neq : AdvancedInstruction
    {
        public neq(byte? resultCell, byte? cell1, byte? cell2)
            : base(IID.neq, resultCell, cell1, cell2) { }
    }
}