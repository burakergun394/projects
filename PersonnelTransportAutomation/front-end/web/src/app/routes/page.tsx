'use client';

import React, { useState } from 'react';
import { Layout, Button, Card, List, Typography, Badge, Space, Empty } from 'antd';
import { EnvironmentOutlined, TeamOutlined, UserOutlined } from '@ant-design/icons';
import { generateRoutes, Personnel, Route } from '@/services/routingService';
import OneMap from '@/components/map/OneMap';
import { MOCK_PERSONNEL } from '@/data/mockPersonnel';

const { Header, Sider, Content } = Layout;
const { Title, Text } = Typography;

/**
 * DASHBOARD PAGE
 * Context Level 7: UI interacting with Routing Service.
 */

export default function RoutesPage() {
  // State: Pure UI State
  const [personnel] = useState<Personnel[]>(MOCK_PERSONNEL);
  const [routes, setRoutes] = useState<Route[]>([]);
  const [selectedRouteId, setSelectedRouteId] = useState<string | null>(null);

  // Handler: Trigger Service
  const handleGenerateRoutes = () => {
    // 1. Call Service
    const result = generateRoutes({ 
        personnel, 
        vehicleCapacity: 4 // Small capacity to show multiple routes for MVP
    });
    
    // 2. Update State
    setRoutes(result.routes);
    setSelectedRouteId(null); // Reset selection
  };

  return (
    <Layout style={{ height: '100vh' }}>
      <Header style={{ display: 'flex', alignItems: 'center', background: '#001529', padding: '0 20px' }}>
        <EnvironmentOutlined style={{ fontSize: '24px', color: '#1890ff', marginRight: '10px' }} />
        <Title level={4} style={{ color: 'white', margin: 0 }}>
          Personnel Transport Automation
        </Title>
      </Header>
      
      <Layout>
        {/* Left Side: Map */}
        <Content style={{ position: 'relative', height: '100%' }}>
            <OneMap 
                personnel={personnel} 
                routes={routes} 
                selectedRouteId={selectedRouteId} 
            />
            
            {/* Overlay Controls */}
            <div style={{ position: 'absolute', top: 20, left: 20, zIndex: 10 }}>
                 <Button 
                    type="primary" 
                    size="large" 
                    icon={<EnvironmentOutlined />}
                    onClick={handleGenerateRoutes}
                    disabled={routes.length > 0}
                    style={{ boxShadow: '0 4px 6px rgba(0,0,0,0.1)' }}
                 >
                    {routes.length > 0 ? 'Routes Generated' : 'Generate Optimized Routes'}
                 </Button>
            </div>
        </Content>

        {/* Right Side: Route List */}
        <Sider width={400} theme="light" style={{ borderLeft: '1px solid #f0f0f0', overflow: 'auto' }}>
            <div style={{ padding: '20px' }}>
                <Title level={5}>
                    <TeamOutlined /> Route Plan ({routes.length})
                </Title>
                
                {routes.length === 0 ? (
                    <Empty description="No routes generated explicitly" />
                ) : (
                    <Space direction="vertical" style={{ width: '100%' }}>
                        {routes.map((route) => (
                            <Card 
                                key={route.id} 
                                size="small"
                                title={
                                    <Space>
                                        <Badge status={selectedRouteId === route.id || !selectedRouteId ? 'processing' : 'default'} />
                                        <span>{route.name}</span>
                                    </Space>
                                }
                                extra={<Badge count={route.personnel.length} style={{ backgroundColor: '#52c41a' }} />}
                                onClick={() => setSelectedRouteId(route.id)}
                                style={{ 
                                    cursor: 'pointer', 
                                    borderColor: selectedRouteId === route.id ? '#1890ff' : '#f0f0f0',
                                    backgroundColor: selectedRouteId === route.id ? '#e6f7ff' : 'white'
                                }}
                                hoverable
                            >
                                <List
                                    size="small"
                                    dataSource={route.personnel}
                                    renderItem={(p) => (
                                        <List.Item>
                                            <List.Item.Meta
                                                avatar={
                                                    <Badge count={p.pickupOrder} style={{ backgroundColor: '#1890ff' }} />
                                                }
                                                title={<Text strong>{p.fullName}</Text>}
                                                description={
                                                    <Space style={{ fontSize: '10px', color: '#888' }}>
                                                        <EnvironmentOutlined /> {p.address} ({p.location.lat.toFixed(4)}, {p.location.lng.toFixed(4)})
                                                    </Space>
                                                }
                                            />
                                        </List.Item>
                                    )}
                                />
                            </Card>
                        ))}
                    </Space>
                )}
            </div>
        </Sider>
      </Layout>
    </Layout>
  );
}
