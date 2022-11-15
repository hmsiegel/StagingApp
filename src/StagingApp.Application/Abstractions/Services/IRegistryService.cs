﻿namespace StagingApp.Application.Abstractions.Services;
public interface IRegistryService
{
    void SetRegistryKeyAndValue(string subKeyName, string name, string value);
    void EditRegistryFromValues(RegistryHive hiveType, string key, string value, string data, RegistryValueKind dataType);
}