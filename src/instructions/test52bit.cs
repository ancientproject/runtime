namespace ancient.runtime
{
    public class test52bit : Instruction
    {
        public test52bit() : base(IID.reserved1)
        {
            
        }
        protected override void OnCompile()
        {
            Construct(1,2,3,4,5,6,7,8,9,10,11,12);
        }
    }
}