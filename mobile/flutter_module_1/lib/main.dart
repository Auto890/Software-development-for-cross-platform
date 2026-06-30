import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'product_detail_screen.dart'; // ดึงไฟล์หน้าจอรายละเอียดเข้ามาใช้งาน

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false, // ปิดแถบแดงคำว่า Debug มุมขวา
      home: JokeListScreen(),
    );
  }
}

class JokeListScreen extends StatefulWidget {
  const JokeListScreen({super.key});

  @override
  State<JokeListScreen> createState() => _JokeListScreenState();
}

class _JokeListScreenState extends State<JokeListScreen> {
  List _jokesList = []; 
  bool _isLoading = false;
  String _errorMessage = "";

  Future<void> fetchJokes() async {
    setState(() {
      _isLoading = true;
      _errorMessage = ""; 
    });

    final url = Uri.parse('http://10.0.2.2:5000/api/Products'); 

    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        final List data = jsonDecode(response.body);
        setState(() {
          _jokesList = data;
          _isLoading = false;
        });
      } else {
        setState(() {
          _errorMessage = "เกิดข้อผิดพลาด: ${response.statusCode}";
          _isLoading = false;
        });
      }
    } catch (e) {
      setState(() {
        _errorMessage = "ไม่สามารถเชื่อมต่อเครื่องคอมพิวเตอร์ (Localhost) ได้";
        _isLoading = false;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    fetchJokes(); 
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Data List จาก Localhost'),
        backgroundColor: Colors.blueAccent,
        foregroundColor: Colors.white,
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator()) 
          : _errorMessage.isNotEmpty
              ? Center(child: Text(_errorMessage, style: const TextStyle(color: Colors.red))) 
              : _jokesList.isEmpty
                  ? const Center(child: Text('ไม่มีข้อมูลในระบบ')) 
                  : ListView.builder( 
                      itemCount: _jokesList.length,
                      itemBuilder: (context, index) {
                        final item = _jokesList[index];

                        return Card(
                          margin: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                          elevation: 2,
                          child: ListTile(
                            leading: CircleAvatar(
                              backgroundColor: Colors.blueAccent,
                              child: Text('${index + 1}', style: const TextStyle(color: Colors.white)),
                            ),
                            title: Text(
                              item['name'] ?? 'ไม่มีข้อความ',
                              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
                            ),
                            subtitle: Padding(
                              padding: const EdgeInsets.only(top: 6.0),
                              child: Text(
                                item['description'] ?? '',
                                style: TextStyle(color: Colors.grey[700], fontStyle: FontStyle.italic),
                                maxLines: 1,
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                            trailing: const Icon(Icons.arrow_forward_ios, size: 14, color: Colors.grey),
                            
                            // 💡 เมื่อกดที่แผ่นข้อมูล จะสั่ง Navigator เปลี่ยนหน้าทันที
                            onTap: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => ProductDetailScreen(product: item),
                                ),
                              );
                            },
                          ),
                        );
                      },
                    ),
      floatingActionButton: FloatingActionButton(
        onPressed: fetchJokes, 
        child: const Icon(Icons.refresh),
      ),
    );
  }
}