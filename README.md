# KoiFarmShop_BE

## ✍️ Description
* The APIs for **Group Project PRN231**
* Using `Swagger` or `Postman` to test APIs.
* 👉 [Click here](#) to view Backend API on Azure.
* 👉 [Click here](#) to view Web on Vercel.

## 💻 Tech Stack

* **Backend:** .NET
* **Database:** MSSQL

# 🔥 How to run & generate database
* Clone project to your computer.
```
git clone https://github.com/PRN231-KoiFarmShop/KoiFarmShop_BE.git

```
* Change `connection string` in *appsettings.Development.json*.
* Set `ks.webapi` as start up project.
* Build & Run (Database will auto generate after run).
* The project default start at `http://localhost:5000/swagger/index.html`

# 🌱 Seeding Data
* Execute api `/api/index` in swagger (only execute 1 time).

# 💳 How to payment with VNPAY
* Execute api `/api/orders/{id}/vnpay` (import OrderId param).
* The response will return the link, use that link to do payment with below informations.
* Card Info:

| Thông tin      | Giá trị             |
| :--------------| :------------------ |
| Ngân hàng      | NCB                 |
| Số thẻ         | 9704198526191432198 |
| Tên chủ thẻ    | NGUYEN VAN A        |
| Ngày phát hành | 07/15               |
| Mật khẩu OTP   | 123456              |

* If payment successfully, the status IsSuccess of Order will be update to 1, and create new record at Payment table.
* **Dont need** to execute api `/api/payments/vn-pay/response` (**API auto run**)
