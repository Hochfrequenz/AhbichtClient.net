namespace AhbichtFunctionsClient.Model;

public class AhbExpressionEvaluationResult
{
    public FormatConstraintEvaluationResult FormatConstraintEvaluationResult { get; set; }
    public RequirementConstraintEvaluationResult RequirementConstraintEvaluationResult { get; set; }
    public RequirementIndicator RequirementIndicator { get; set; }
}
/// <summary>
/// Possible values to describe the state of a condition
/// in the condition_fulfilled attribute of the ConditionNodes.
/// </summary>
public enum ConditionFulfilledValue
{
    /// <summary>
    /// If condition is fulfilled
    /// </summary>
    Fulfilled = 1,

    /// <summary>
    /// If condition is not fulfilled
    /// </summary>
    Unfulfilled,

    /// <summary>
    /// If it cannot be checked if condition is fulfilled (e.g. "Wenn vorhanden")
    /// </summary>
    Unknown,

    /// <summary>
    /// A hint or unevaluated format constraint which does not have a status of being fulfilled or not
    /// </summary>
    Neutral
}

/// <summary>
/// A modal mark describes if information are obligatory or not. The German term is "Merkmal".
/// The modal marks are defined by the EDI Energy group (see edi-energy.de → Dokumente → Allgemeine Festlegungen).
/// The modal mark stands alone or before a condition expression.
/// It can be the start of several requirement indicator expressions in one AHB expression.
/// </summary>
public enum ModalMark
{
    /// <summary>
    /// German term for "Must". Is required for the correct structure of the message.
    /// If the following condition is not fulfilled, the information must not be given ("must not")
    /// </summary>
    Muss,

    /// <summary>
    /// German term for "Should". Is required for technical reasons.
    /// Always followed by a condition.
    /// If the following condition is not fulfilled, the information must not be given.
    /// </summary>
    Soll,

    /// <summary>
    /// German term for "Can". Optional
    /// </summary>
    Kann
}

/// <summary>
/// Operator which does not function to combine conditions, but as requirement indicator.
/// It stands alone or in front of a condition expression. Please find detailed descriptions of the operators and their
/// usage in the "Allgemeine Festlegungen".
/// Note that with MaKo2022 introced 2022-04-01 the "O" and "U" prefix operators will be deprecated.
/// Refer to the "Allgemeine Festlegungen" valid up to 2022-04-01 for deprecated "O" and "U".
/// </summary>
public enum PrefixOperator
{
    /// <summary>
    /// The "X" operator. See "Allgemeine Festlegungen" Kapitel 6.8.1. Usually this just means something is required
    /// or required under circumstances defined in a trailing condition expression.
    /// It shall be read as "exclusive or" regarding how qualifiers/codes shall be used from a finite set.
    /// Note that "X" can also be used as "logical exclusive or" (aka "xor") operator in condition expressions.
    /// The prefix operator works differently from the logical operator in condition expressions!
    /// The usage of "X" as logical operator is deprecated since 2022-04-01. It will be replaced with the "⊻" symbol.
    /// </summary>
    X,

    /// <summary>
    /// The "O" operator means that at least one out of multiple possible qualifiers/codes has to be given.
    /// This is typically found when describing ways to contact a market partner (CTA): You can use email or phone or fax
    /// but you have to provide at least one of the given possibilities.
    /// The usage of "O" as a prefix operator is deprecated since 2022-04-01.
    /// Note that "O" can also be used as a "logical or" (aka "lor") operator in condition expressions.
    /// The prefix operator works differently from the logical operator in condition expressions!
    /// The usage of "O" as logical operator is also deprecated since 2022-04-01. It will be replaced with the "∨" symbol.
    /// </summary>
    O,

    /// <summary>
    /// The "U" operator means that all provided qualifiers/codes have to be used.
    /// The usage of "U" as a prefix operator is deprecated since 2022-04-01.
    /// Note that "U" can also be used as a "logical and" (aka "land") operator in condition expressions.
    /// The prefix operator works differently from the logical operator in condition expressions!
    /// The usage of "U" as logical operator is also deprecated since 2022-04-01. It will be replaced with the "∧" symbol.
    /// </summary>
    U
}

/// <summary>
/// A Requirement Indicator is either the Merkmal :class:`ModalMark` or the :class:`PrefixOperator` of the data element/data element group/segment/segment group at which it is used.
/// </summary>
public enum RequirementIndicator
{
    /// <summary>
    /// German term for "Must". Is required for the correct structure of the message.
    /// If the following condition is not fulfilled, the information must not be given ("must not")
    /// </summary>
    Muss,

    /// <summary>
    /// German term for "Should". Is required for technical reasons.
    /// Always followed by a condition.
    /// If the following condition is not fulfilled, the information must not be given.
    /// </summary>
    Soll,

    /// <summary>
    /// German term for "Can". Optional
    /// </summary>
    Kann,
    /// <summary>
    /// The "X" operator. See "Allgemeine Festlegungen" Kapitel 6.8.1. Usually this just means something is required
    /// or required under circumstances defined in a trailing condition expression.
    /// It shall be read as "exclusive or" regarding how qualifiers/codes shall be used from a finite set.
    /// Note that "X" can also be used as "logical exclusive or" (aka "xor") operator in condition expressions.
    /// The prefix operator works differently from the logical operator in condition expressions!
    /// The usage of "X" as logical operator is deprecated since 2022-04-01. It will be replaced with the "⊻" symbol.
    /// </summary>
    X,

    /// <summary>
    /// The "O" operator means that at least one out of multiple possible qualifiers/codes has to be given.
    /// This is typically found when describing ways to contact a market partner (CTA): You can use email or phone or fax
    /// but you have to provide at least one of the given possibilities.
    /// The usage of "O" as a prefix operator is deprecated since 2022-04-01.
    /// Note that "O" can also be used as a "logical or" (aka "lor") operator in condition expressions.
    /// The prefix operator works differently from the logical operator in condition expressions!
    /// The usage of "O" as logical operator is also deprecated since 2022-04-01. It will be replaced with the "∨" symbol.
    /// </summary>
    O,

    /// <summary>
    /// The "U" operator means that all provided qualifiers/codes have to be used.
    /// The usage of "U" as a prefix operator is deprecated since 2022-04-01.
    /// Note that "U" can also be used as a "logical and" (aka "land") operator in condition expressions.
    /// The prefix operator works differently from the logical operator in condition expressions!
    /// The usage of "U" as logical operator is also deprecated since 2022-04-01. It will be replaced with the "∧" symbol.
    /// </summary>
    U
}
