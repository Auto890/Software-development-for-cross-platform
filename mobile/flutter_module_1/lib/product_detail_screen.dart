import 'package:flutter/material.dart';

class ProductDetailScreen extends StatelessWidget {
  final Map product; // ท่อรับข้อมูลสินค้าชิ้นที่ถูกกดส่งข้ามมาจากหน้าแรก

  const ProductDetailScreen({super.key, required this.product});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(product['name'] ?? 'รายละเอียดสินค้า'),
        backgroundColor: Colors.blueAccent,
        foregroundColor: Colors.white,
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              product['name'] ?? 'ไม่มีชื่อสินค้า',
              style: const TextStyle(fontSize: 26, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 12),
            Row(
              children: [
                const Text('ราคา: ', style: TextStyle(fontSize: 18, color: Colors.grey)),
                Text(
                  '฿${product['price'] ?? 0}',
                  style: const TextStyle(fontSize: 22, color: Colors.green, fontWeight: FontWeight.bold),
                ),
              ],
            ),
            const SizedBox(height: 8),
            Row(
              children: [
                const Text('คงเหลือในคลัง: ', style: TextStyle(fontSize: 16, color: Colors.grey)),
                Text(
                  '${product['stock'] ?? 0} ชิ้น',
                  style: const TextStyle(fontSize: 16, color: Colors.orange, fontWeight: FontWeight.bold),
                ),
              ],
            ),
            const Divider(height: 40, thickness: 1.2),
            const Text(
              'คำอธิบายรายละเอียดสินค้า:',
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 8),
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(12),
              decoration: BoxDecoration(
                color: Colors.grey[100],
                borderRadius: BorderRadius.circular(8),
                border: Border.all(color: Colors.grey[300]!),
              ),
              child: Text(
                product['description'] != null && product['description'].toString().isNotEmpty
                    ? product['description']
                    : 'ไม่มีคำอธิบายสำหรับสินค้านี้',
                style: const TextStyle(fontSize: 16, height: 1.4, color: Colors.black87),
              ),
            ),
          ],
        ),
      ),
    );
  }
}