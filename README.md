# csharpservicebinding
> **Kubernetes Service Binding Library for C# Applications**

The [Service Binding Specification][spec] for Kubernetes standardizes exposing
backing service secrets to applications.  This project provides a C# module
to consume the bindings projected into the container.  The [Application
Projection][application-projection] section of the spec describes how the
bindings are projected into the application.  The primary mechanism of
projection is through files mounted at a specific directory.  The bindings
directory path can be discovered through `SERVICE_BINDING_ROOT` environment
variable.  The operator must have injected `SERVICE_BINDING_ROOT` environment to
all the containers where bindings are created.

Within this service binding root directory, there could be multiple bindings
projected from different Service Bindings.  For example, suppose an application
requires to connect to a database and cache server. In that case, one Service
Binding can provide the database, and the other Service Binding can offer
bindings to the cache server.

The example given in the spec:

$SERVICE_BINDING_ROOT
├── account-database
│   ├── type
│   ├── provider
│   ├── uri
│   ├── username
│   └── password
└── transaction-event-stream
    ├── type
    ├── connection-count
    ├── uri
    ├── certificates
    └── private-key

In the above example, there are two bindings under the `SERVICE_BINDING_ROOT`
directory.  The `account-database` and `transaction-event-system` are the names
of the bindings.  Files within each bindings directory has a special file named
`type`, and you can rely on the value of that file to identify the type of the
binding projected into that directory.  In certain directories, you can also see
another file named `provider`.  The provider is an additional identifier to
narrow down the type further.  This module use the `type` field and, optionally,
`provider` field to look up the bindings.

## Installation

## Usage

[spec]: https://github.com/k8s-service-bindings/spec
[application-projection]: https://github.com/k8s-service-bindings/spec#application-projection