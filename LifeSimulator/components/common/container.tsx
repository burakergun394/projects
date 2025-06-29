import React from 'react';
import { ViewStyle } from 'react-native';
import styled from 'styled-components/native';

interface ContainerProps {
  children: React.ReactNode;
  // Layout
  direction?: 'row' | 'column';
  justify?: 'flex-start' | 'flex-end' | 'center' | 'space-between' | 'space-around' | 'space-evenly';
  align?: 'flex-start' | 'flex-end' | 'center' | 'stretch' | 'baseline';
  // Spacing
  padding?: number;
  paddingHorizontal?: number;
  paddingVertical?: number;
  margin?: number;
  marginHorizontal?: number;
  marginVertical?: number;
  gap?: number;
  // Size
  width?: string | number;
  height?: string | number;
  flex?: number;
  // Background
  backgroundColor?: string;
  // Border
  borderRadius?: number;
  // Shadow
  shadow?: boolean;
  // Blur
  blur?: boolean;
  // Style override
  style?: ViewStyle;
}

const StyledContainer = styled.View<ContainerProps>`
  ${(props) => `flex-direction: ${props.direction || 'column'};`}
  ${(props) => `justify-content: ${props.justify || 'flex-start'};`}
  ${(props) => `align-items: ${props.align || 'stretch'};`}
  
  ${(props) => props.padding && `padding: ${props.padding}px;`}
  ${(props) => props.paddingHorizontal && `padding-horizontal: ${props.paddingHorizontal}px;`}
  ${(props) => props.paddingVertical && `padding-vertical: ${props.paddingVertical}px;`}
  ${(props) => props.margin && `margin: ${props.margin}px;`}
  ${(props) => props.marginHorizontal && `margin-horizontal: ${props.marginHorizontal}px;`}
  ${(props) => props.marginVertical && `margin-vertical: ${props.marginVertical}px;`}
  ${(props) => props.gap && `gap: ${props.gap}px;`}
  
  ${(props) => props.width && `width: ${typeof props.width === 'number' ? `${props.width}px` : props.width};`}
  ${(props) => props.height && `height: ${typeof props.height === 'number' ? `${props.height}px` : props.height};`}
  ${(props) => props.flex && `flex: ${props.flex};`}
  
  ${(props) => props.backgroundColor && `background-color: ${props.backgroundColor};`}
  ${(props) => props.borderRadius && `border-radius: ${props.borderRadius}px;`}
  
  ${(props) => props.shadow && `
    shadow-color: #000000;
    shadow-offset: 0px 4px;
    shadow-opacity: 0.1;
    shadow-radius: 8px;
    elevation: 4;
  `}
  
  ${(props) => props.blur && `backdrop-filter: blur(15px);`}
`;

export const Container: React.FC<ContainerProps> = ({ children, style, ...props }) => {
  return (
    <StyledContainer {...props} style={style}>
      {children}
    </StyledContainer>
  );
}; 