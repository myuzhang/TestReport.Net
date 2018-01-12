<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="7487240c-cbc6-4d66-8284-d411f4e9ab64" namespace="TestReport.SpecFlow.Configuration" xmlSchemaNamespace="urn:TestReport.SpecFlow.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="SpecFlowReportSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="specFlow.Report">
      <elementProperties>
        <elementProperty name="MailSettings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mailSettings" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/MailSettingsElement" />
          </type>
        </elementProperty>
        <elementProperty name="ReportSettings" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="reportSettings" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/ReportSettingsElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="MailSettingsElement">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Username" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="username" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Password" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="password" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Subject" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="subject" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnableSsl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enableSsl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="FromAddress" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="fromAddress" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ToAddresses" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="toAddresses" isReadOnly="false" documentation="Seperate by ';'">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Enabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="ReportSettingsElement">
      <attributeProperties>
        <attributeProperty name="Path" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="path" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7487240c-cbc6-4d66-8284-d411f4e9ab64/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>