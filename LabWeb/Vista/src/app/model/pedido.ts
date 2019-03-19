import { detallePedido } from "./detallePedido";

export class pedido{
    CustomerID: string;
    CustomerName: string;
    OrderID: string;
    OrderDate: Date;
    RequiredDate: Date;
    ShippedDate: Date;
    Freight: number;
    public Order_Details: detallePedido[];
}