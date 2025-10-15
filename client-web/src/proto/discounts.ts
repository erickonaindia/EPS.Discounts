import {
  Message,
  proto3,
  FieldList,
  ScalarType,
  type BinaryReadOptions,
  type JsonReadOptions,
} from "@bufbuild/protobuf";
import type { ServiceType } from "@bufbuild/protobuf";
import { MethodKind } from "@bufbuild/protobuf";

export class GenerateRequest extends Message<GenerateRequest> {
  count = 0;
  length = 0;
  constructor(data?: Partial<GenerateRequest>) {
    super();
    proto3.util.initPartial(data, this);
  }
  static readonly runtime = proto3;
  static readonly typeName = "discounts.GenerateRequest";
  static readonly fields: FieldList = proto3.util.newFieldList(() => [
    { no: 1, name: "count", kind: "scalar", T: ScalarType.UINT32 },
    { no: 2, name: "length", kind: "scalar", T: ScalarType.UINT32 },
  ]);

  static fromBinary(bytes: Uint8Array, options?: Partial<BinaryReadOptions>) {
    return new GenerateRequest().fromBinary(bytes, options);
  }
  static fromJson(json: unknown, options?: Partial<JsonReadOptions>) {
    return new GenerateRequest().fromJson(json, options);
  }
  static fromJsonString(json: string, options?: Partial<JsonReadOptions>) {
    return new GenerateRequest().fromJsonString(json, options);
  }
}

export class GenerateResponse extends Message<GenerateResponse> {
  result = false;
  constructor(data?: Partial<GenerateResponse>) {
    super();
    proto3.util.initPartial(data, this);
  }
  static readonly runtime = proto3;
  static readonly typeName = "discounts.GenerateResponse";
  static readonly fields: FieldList = proto3.util.newFieldList(() => [
    { no: 1, name: "result", kind: "scalar", T: ScalarType.BOOL },
  ]);

  static fromBinary(bytes: Uint8Array, options?: Partial<BinaryReadOptions>) {
    return new GenerateResponse().fromBinary(bytes, options);
  }
  static fromJson(json: unknown, options?: Partial<JsonReadOptions>) {
    return new GenerateResponse().fromJson(json, options);
  }
  static fromJsonString(json: string, options?: Partial<JsonReadOptions>) {
    return new GenerateResponse().fromJsonString(json, options);
  }
}

export class UseCodeRequest extends Message<UseCodeRequest> {
  code = "";
  constructor(data?: Partial<UseCodeRequest>) {
    super();
    proto3.util.initPartial(data, this);
  }
  static readonly runtime = proto3;
  static readonly typeName = "discounts.UseCodeRequest";
  static readonly fields: FieldList = proto3.util.newFieldList(() => [
    { no: 1, name: "code", kind: "scalar", T: ScalarType.STRING },
  ]);

  static fromBinary(bytes: Uint8Array, options?: Partial<BinaryReadOptions>) {
    return new UseCodeRequest().fromBinary(bytes, options);
  }
  static fromJson(json: unknown, options?: Partial<JsonReadOptions>) {
    return new UseCodeRequest().fromJson(json, options);
  }
  static fromJsonString(json: string, options?: Partial<JsonReadOptions>) {
    return new UseCodeRequest().fromJsonString(json, options);
  }
}

export class UseCodeResponse extends Message<UseCodeResponse> {
  resultCode = 0;
  constructor(data?: Partial<UseCodeResponse>) {
    super();
    proto3.util.initPartial(data, this);
  }
  static readonly runtime = proto3;
  static readonly typeName = "discounts.UseCodeResponse";
  static readonly fields: FieldList = proto3.util.newFieldList(() => [
    { no: 1, name: "result_code", jsonName: "resultCode", kind: "scalar", T: ScalarType.UINT32 },
  ]);

  static fromBinary(bytes: Uint8Array, options?: Partial<BinaryReadOptions>) {
    return new UseCodeResponse().fromBinary(bytes, options);
  }
  static fromJson(json: unknown, options?: Partial<JsonReadOptions>) {
    return new UseCodeResponse().fromJson(json, options);
  }
  static fromJsonString(json: string, options?: Partial<JsonReadOptions>) {
    return new UseCodeResponse().fromJsonString(json, options);
  }
}

export const DiscountService = {
  typeName: "discounts.DiscountService",
  methods: {
    generate: { name: "Generate", I: GenerateRequest, O: GenerateResponse, kind: MethodKind.Unary },
    useCode:  { name: "UseCode",  I: UseCodeRequest,  O: UseCodeResponse,  kind: MethodKind.Unary },
  },
} as const satisfies ServiceType;