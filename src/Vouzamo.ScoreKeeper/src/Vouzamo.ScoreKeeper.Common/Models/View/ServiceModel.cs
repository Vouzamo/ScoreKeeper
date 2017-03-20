namespace Vouzamo.ScoreKeeper.Common.Models.View
{
    public class ServiceModel<TInput, TOutput>
    {
        public TInput Input { get; protected set; }
        public TOutput Output { get; protected set; }

        public ServiceModel(TInput input, TOutput output)
        {
            Input = input;
            Output = output;
        }
    }
}
