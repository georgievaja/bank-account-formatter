namespace BankAccountFormatter.Interpreter
open BankAccountFormatter.Parser.AbstractSyntaxTree
open BankAccountFormatter.Types.BankAccount.PublicTypes
open BankAccountFormatter.Types.BankAccountConstants

module Types =
    type FormattingFunction = BankAccount -> string

module Interpreter =
    open Types
    
    let interpretPrefix (prefix: Prefix) : FormattingFunction =
        match prefix with
        | MinimizedPrefix -> (fun ds -> ds.Prefix.ToString())
        | PaddedPrefix -> (fun ds -> ds.Prefix.ToString().PadLeft(PrefixPaddingPlaces, '0'))
    
    let interpretBankCode (bankCode: BankCode) : FormattingFunction =
        match bankCode with
        | MinimizedBankCode -> (fun ds -> ds.BankCode.ToString())
        | PaddedBankCode -> (fun ds -> ds.BankCode.ToString().PadLeft(BankCodePaddingPlaces, '0'))
        
    let interpretAccountNumber (accountNumber: AccountNumber) : FormattingFunction =
        match accountNumber with
        | MinimizedAccountNumber -> (fun ds -> ds.AccountNumber.ToString())
        | PaddedAccountNumber -> (fun ds -> ds.AccountNumber.ToString().PadLeft(AccountNumberPaddingPlaces, '0'))
        
    let interpretBankAccountPart (bankAccountPart: BankAccountPart) : FormattingFunction =
        match bankAccountPart with
        | Prefix p -> interpretPrefix p
        | AccountNumber an -> interpretAccountNumber an
        | BankCode bc -> interpretBankCode bc
    
    let interpretSeparator (separator: Separator) : FormattingFunction =
        match separator with
        | BankCodeSeparator -> (fun _ -> BankCodeSeparatorLiteral)
        | PrefixSeparator -> (fun _ -> PrefixSeparatorLiteral)

    let interpretOtherChar (otherChar: char) : FormattingFunction =
         fun _ -> otherChar.ToString()

    let interpretBankAccountFormatPart (part : BankAccountFormatPart) : FormattingFunction =
        match part with
        | BankAccountPart ba -> interpretBankAccountPart ba
        | BankAccountSeparator se -> interpretSeparator se
        | OtherChar oc -> interpretOtherChar oc
        
    let interpret (format: BankAccountFormat) : FormattingFunction =
        fun domesticBankAccount ->
        match format with
            | BankAccountFormatParts bankAccountFormatParts ->
                   bankAccountFormatParts
                    |> List.map interpretBankAccountFormatPart        
                    |> List.map (fun x -> x domesticBankAccount)
                    |> List.fold (+) ""
                
