"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const TDataResult_1 = __importDefault(require("./TDataResult"));
class DataResult extends TDataResult_1.default {
    static fromJson(json) {
        const dataResult = new DataResult();
        Object.assign(dataResult, json);
        return dataResult;
    }
}
exports.default = DataResult;
