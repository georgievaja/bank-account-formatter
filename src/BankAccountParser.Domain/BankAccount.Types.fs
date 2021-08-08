namespace BankAccountParser.Teams
open System

module BankAccount =

    module PublicTypes =
        
        type BankAccount = 
            {
                Prefix: int32
                AccountNumber: int64
                BankCode: int16
            }

    module Monads =
        type OperationFunctionResult<'TSuccess, 'TFailure> = 
            | Success of 'TSuccess
            | Failure of 'TFailure
    
        let map f res =
            match res with      
                |Success (x, log) -> Success (f x, log)
                |Failure (x, log) -> Failure (x, log)
    
        let (<!>) x f = map f x

        let bind exprFunction xOpt  = 
            match xOpt with
            | Success (r, log) -> match exprFunction r with
                                    | Success (r2, log2) -> Success (r2, log @ log2)
                                    | Failure (x, log2) -> Failure (x, log @ log2)
            | Failure (x, log) -> Failure (x, log)

        let (>>=) f x = bind f x

        let (>=>) f1 f2 arg = 
            match f1 arg with
            | Success (x, log) -> match f2 x with      
                                    |Success (x2, log2) -> Success (x2, (log @ log2))
                                    |Failure (x2, log2) -> Failure (x2, (log @ log2))
            | Failure (x, log) -> Failure (x, log)
