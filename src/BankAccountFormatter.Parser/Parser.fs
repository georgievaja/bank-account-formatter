namespace BankAccountFormatter.Parser

open FParsec
open AbstractSyntaxTree
open ParserTypes.Monads
open ParserTypes.Types
open BankPartParsers
open SeparatorParsers
open CharParsers

module Parser =
               
    let pBankAccountFormatQuery : Parser<BankAccountFormatPart, unit> = 
        choice [
            pBankAccountPart
            pSeparator
            pOtherChar
        ]

    let pBankAccountFormatFailFatally: Parser<BankAccountFormatPart, _> =
        fun stream ->
            let reply = pBankAccountFormatQuery stream
            match reply.Status with
                | Ok -> Reply(reply.Result)             
                | _ -> Reply(FatalError, reply.Error)
  
    let pBankAccountFormat : Parser<BankAccountFormat, unit> = 
        manyTill pBankAccountFormatFailFatally eof
        |>> BankAccountFormatParts

    let parse (rule: string) : ParsingResult<BankAccountFormat, OperationLog> = 
      match run pBankAccountFormat rule with
       | ParserResult.Success (result, _, _) ->
           ParsingResult.Success result
       | ParserResult.Failure (_, error, _) -> 
            SyntacticLog.create rule error
            |> Syntactic
            |> ParsingResult.Failure

                                                    

