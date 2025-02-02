interface JwtPayload {
  username: string;
  tenant: string;
  exp: number;
}

export const createDummyJwt = (username: string, tenant: string): string => {
  // Real JWT'de bu bilgiler şifrelenir, burada base64 encoding kullanıyoruz
  const header = btoa(JSON.stringify({ alg: "HS256", typ: "JWT" }));
  
  const payload: JwtPayload = {
    username,
    tenant,
    exp: Date.now() + 24 * 60 * 60 * 1000, // 24 saat
  };
  
  const payloadBase64 = btoa(JSON.stringify(payload));
  const signature = btoa("dummy-signature"); // Gerçek uygulamada imzalama yapılır

  return `${header}.${payloadBase64}.${signature}`;
};

export const parseJwt = (token: string): JwtPayload | null => {
  try {
    const base64Payload = token.split('.')[1];
    const payload = JSON.parse(atob(base64Payload));
    return payload;
  } catch {
    return null;
  }
};

export const isTokenExpired = (token: string): boolean => {
  const payload = parseJwt(token);
  if (!payload) return true;
  return Date.now() > payload.exp;
}; 