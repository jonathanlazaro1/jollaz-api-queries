import TDataResult from "./TDataResult";

export default class DataResult extends TDataResult<any> {
  static fromJson(json: JSON): DataResult {
    const dataResult = new DataResult();
    Object.assign(dataResult, json);
    return dataResult;
  }
}
