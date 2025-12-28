'use client';

import React, { useState } from 'react';
import { Layout, Button, Card, List, Typography, Badge, Space, Empty, Select, message } from 'antd';
import { EnvironmentOutlined, TeamOutlined, UserOutlined, ShopOutlined } from '@ant-design/icons';
import { generateRoutes, Personnel, Route } from '@/services/routingService';
import { MOCK_PERSONNEL } from '@/data/mockPersonnel';
import { Destination, MOCK_DESTINATIONS } from '@/data/mockDestinations';
import dynamic from 'next/dynamic';

const { Header, Sider, Content } = Layout;
const { Title, Text } = Typography;
const { Option } = Select;

// Dynamically import Map to disable SSR (Google Maps is client-side only)
const OneMap = dynamic(() => import('@/components/map/OneMap'), { 
    ssr: false,
    loading: () => <div className="h-full w-full bg-gray-100 flex items-center justify-center">Loading Map...</div>
});

/**
 * DASHBOARD PAGE
 * Context Level 7: UI interacting with Routing Service.
 */

export default function RoutesPage() {
  // State: Pure UI State
  const [personnel] = useState<Personnel[]>(MOCK_PERSONNEL);
  const [destinations] = useState<Destination[]>(MOCK_DESTINATIONS);
  const [selectedDestination, setSelectedDestination] = useState<Destination | null>(null);
  
  const [routes, setRoutes] = useState<Route[]>([]);
  const [selectedRouteId, setSelectedRouteId] = useState<string | null>(null);

  // Handler: Trigger Service
  const handleGenerateRoutes = () => {
    if (!selectedDestination) {
        message.error('Please select a destination first.');
        return;
    }

    // 1. Call Service with Destination
    const result = generateRoutes({ 
        personnel, 
        destination: selectedDestination,
        vehicleCapacity: 4 // Small capacity to show multiple routes for MVP
    });
    
    // 2. Update State
    setRoutes(result.routes);
    setSelectedRouteId(null); // Reset selection
    message.success(`Generated ${result.totalRoutes} routes to ${selectedDestination.name}`);
  };

  const handleDestinationChange = (value: string) => {
      const dest = destinations.find(d => d.id === value) || null;
      setSelectedDestination(dest);
      setRoutes([]); // Clear routes on destination change
  };

  return (
    <Layout style={{ height: '100vh' }}>
      <Header style={{ display: 'flex', alignItems: 'center', background: '#001529', padding: '0 20px', justifyContent: 'space-between' }}>
        <div style={{ display: 'flex', alignItems: 'center' }}>
            <EnvironmentOutlined style={{ fontSize: '24px', color: '#1890ff', marginRight: '10px' }} />
            <Title level={4} style={{ color: 'white', margin: 0 }}>
            Personnel Transport Automation
            </Title>
        </div>
        
        {/* Destination Selector in Header */}
        <div style={{ minWidth: 300 }}>
             <Select 
                style={{ width: '100%' }} 
                placeholder="Select Target Destination"
                onChange={handleDestinationChange}
                size="large"
             >
                 {destinations.map(d => (
                     <Option key={d.id} value={d.id}>
                        <ShopOutlined /> {d.name}
                     </Option>
                 ))}
             </Select>
        </div>
      </Header>
      
      <Layout>
        {/* Left Side: Map */}
        <Content style={{ position: 'relative', height: '100%' }}>
            <OneMap 
                personnel={personnel} 
                routes={routes} 
                destination={selectedDestination}
                selectedRouteId={selectedRouteId} 
            />
            
            {/* Overlay Controls */}
            <div style={{ position: 'absolute', top: 20, left: 20, zIndex: 10 }}>
                 <Button 
                    type="primary" 
                    size="large" 
                    icon={<EnvironmentOutlined />}
                    onClick={handleGenerateRoutes}
                    disabled={!selectedDestination}
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
                    <Empty 
                        description={selectedDestination ? "Ready to generate routes" : "Select a destination to start"} 
                        image={Empty.PRESENTED_IMAGE_SIMPLE}
                    />
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
                                                        <EnvironmentOutlined /> {p.address}
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
