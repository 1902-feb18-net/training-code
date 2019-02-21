using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Delegates
{
    public class MoviePlayer
    {
        public Movie CurrentMovie { get; set; }

        // declares a delegate type.
        // all the types in C#: class, struct, interface, enum, delegate
        // delegate type is a name for a function with a particular signature
        //     (return value and parameters)
        // this one is for void-return and zero-parameters.
        public delegate void MovieFinishedHandler();

        // is an event member
        // every event has a delegate type to indicate
        // what kind of functions can be subscribed to the event.
        // "there will be a MovieFinished event, and you can subscribe to it
        // with any MovieFinishedHandler, i.e., and void-return, zero-param function.
        public event MovieFinishedHandler MovieFinished;

        // Action type is for void-returning function.
        //    for Action, the type parameters are all the parameters of the function.
        // Func type is for all other functions.
        //    for Func, the last type parameter is for the return type.
        public event Action<string> DiscEjected;

        // technically we can even subscribe to delegate-typed members without events at all
        // but it's a bit more limited

        // return true if success, false if failure
        public bool Play()
        {
            // wait for 3 seconds
            Thread.Sleep(3000);

            // firing the MovieFinished event
            // must null-check because if there are zero subscribers,
            // that's a nullreferenceexception.
            //if (MovieFinished != null)
            //{
            //    MovieFinished();
            //}
            MovieFinished?.Invoke();
            // this will call not just one function, but all event subscribers.

            // ?. is a special operator called the null-conditional operator
            // if the thing to the left of it is null, it simply returns null.
            // if the thing to the left is NOT null, it will behave like . (dot)

            // ?? is the null-coalescing operator
            // a ?? b, that means, return a, unless it's null, in which case return b.

            // when we fire the event, we provide all parameters that the event
            // says its handlers need.
            DiscEjected?.Invoke(CurrentMovie.ToString());

            return true;
        }
    }
}
