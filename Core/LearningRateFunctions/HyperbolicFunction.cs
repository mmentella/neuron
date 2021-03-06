/***********************************************************************************************
 COPYRIGHT 2008 Vijeth D

 This file is part of NeuronDotNet.
 (Project Website : http://neurondotnet.freehostia.com)

 NeuronDotNet is a free software. You can redistribute it and/or modify it under the terms of
 the GNU General Public License as published by the Free Software Foundation, either version 3
 of the License, or (at your option) any later version.

 NeuronDotNet is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 See the GNU General Public License for more details.

 You should have received a copy of the GNU General Public License along with NeuronDotNet.
 If not, see <http://www.gnu.org/licenses/>.

***********************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace NeuronDotNet.Core.LearningRateFunctions
{
    /// <summary>
    /// Hyperbolic Learning Rate Function. As training progresses, The learning rate hyperbolically
    /// changes from its initial value to the final value.
    /// </summary>
    [Serializable]
    public sealed class HyperbolicFunction : AbstractFunction
    {
        private readonly double product;

        /// <summary>
        /// Constructs a new instance of the hyperbolic function with the specified initial and
        /// final values of learning rate.
        /// </summary>
        /// <param name="initialLearningRate">
        /// Initial value learning rate
        /// </param>
        /// <param name="finalLearningRate">
        /// Final value learning rate
        /// </param>
        public HyperbolicFunction(double initialLearningRate, double finalLearningRate)
            : base(initialLearningRate, finalLearningRate)
        {
            product = initialLearningRate * finalLearningRate;
        }

        /// <summary>
        /// Deserialization Constructor
        /// </summary>
        /// <param name="info">
        /// Serialization information to deserialize and obtain the data
        /// </param>
        /// <param name="context">
        /// Serialization context to use
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// if <c>info</c> is <c>null</c>
        /// </exception>
        public HyperbolicFunction(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            product = initialLearningRate * finalLearningRate;
        }

        /// <summary>
        /// Gets effective learning rate for current training iteration. (As training progresses, The
        /// learning rate hyperbolically changes from its initial value to the final value)
        /// </summary>
        /// <param name="currentIteration">
        /// Current training iteration
        /// </param>
        /// <param name="trainingEpochs">
        /// Total number of training epochs
        /// </param>
        /// <returns>
        /// The effective learning rate for current training iteration
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If <c>trainingEpochs</c> is zero or negative
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <c>currentIteration</c> is negative or, if it is not less than <c>trainingEpochs</c>
        /// </exception>
        public override double GetLearningRate(int currentIteration, int trainingEpochs)
        {
            Helper.ValidatePositive(trainingEpochs, "trainingEpochs");
            Helper.ValidateWithinRange(currentIteration, 0, trainingEpochs - 1, "currentIteration");

            double denominator = finalLearningRate
                + ((initialLearningRate - finalLearningRate) * currentIteration) / trainingEpochs;

            if (denominator == 0)
            {
                return 0d;
            }
            return product / denominator;
        }
    }
}