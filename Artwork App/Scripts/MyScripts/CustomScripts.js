String.prototype.isBlank = function (str)
{
    return (!str || 0 === str.length);
}